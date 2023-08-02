using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    // Enemy health
    [SerializeField] float health, maxHealth = 2f;

    public GameObject player;
    public float speed;
    public float distanceBetween;
    public float attackRange;

    private float distance;
    private Animator animator;
    private EnemyCombat eCombat;

    // New variable to track if the enemy is following the player
    private bool isFollowingPlayer = true;

    private float initialMoveDuration = 2f;
    private float initialMoveTime;

    // Declarations for scoreCounter
    public int value;

    private void Start()
    {
        // Health system
        health = maxHealth;

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found! Make sure the player has the 'Player' tag.");
        }


        eCombat = GetComponent<EnemyCombat>();

        animator = GetComponent<Animator>();

        initialMoveTime = Time.time;
    }

    private void Update()
    {

        if (Time.time <= initialMoveTime + initialMoveDuration)
        {
            // Initial move duration hasn't passed yet, move towards the player
            MoveTowardsPlayer();

        }


        else if (isFollowingPlayer)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();



            eCombat.SetAttackPointPosition(direction);

            // Calculate the movement vector
            Vector2 moveDirection = direction * speed * Time.deltaTime;

            if (distance < distanceBetween)
            {
                // Move the goblin towards the player
                transform.Translate(moveDirection);

                // Play SFX
                AudioManager.Instance.PlaySFX("growl");

                animator.SetBool("isMoving", true);

                // Check if the distance is close enough to attack
                if (distance < attackRange)
                {
                    // Play SFX
                    AudioManager.Instance.PlaySFX("stab");
                    animator.SetBool("isAttacking", true);
                }
                else
                {
                    animator.SetBool("isAttacking", false);
                }
            }
            else
            {
                animator.SetBool("isMoving", false);
                animator.SetBool("isAttacking", false);
            }

            // Update the blend tree parameters
            animator.SetFloat("horizontalMovement", moveDirection.x);
            animator.SetFloat("verticalMovement", moveDirection.y);
        }
    }

        private void MoveTowardsPlayer()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        eCombat.SetAttackPointPosition(direction);
        Vector2 moveDirection = direction * speed * Time.deltaTime;
        transform.Translate(moveDirection);
        animator.SetBool("isMoving", true);

        animator.SetFloat("horizontalMovement", moveDirection.x);
        animator.SetFloat("verticalMovement", moveDirection.y);


    }


    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // 3 -> 2 -> 1 -> 0 = Enemy has died

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Stop following the player when the enemy dies
        isFollowingPlayer = false;

        // Play SFX
        AudioManager.Instance.PlaySFX("goblin death");

        // Debug
        Debug.Log("Enemy Goblin died!");

        // Disable the Collider component to prevent further collisions
        GetComponent<BoxCollider2D>().enabled = false;

        // Play the death animation by setting the "isDead" parameter to true
        animator.SetBool("isDead", true);

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 2f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);

        // Trigger the scoreCount event when the goblin dies
        ScoreCounter.Instance.IncreaseScore(value);
    }
}