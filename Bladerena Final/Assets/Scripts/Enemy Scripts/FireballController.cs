using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    // Enemy health
    [SerializeField] float health, maxHealth = 2f;

    public GameObject player;
    public float speed;
    public float distanceBetween;

    private float distance;
    private Animator animator;

    // New variable to track if the enemy is following the player
    private bool isFollowingPlayer = true;

    // Declarations for scoreCount
    public int value;

    private GameObject HomingSFX; 
    private GameObject BurningSFX; 
    private GameObject ExplodingSFX; 

    private AudioSource audSourceHoming;
    private AudioSource audioSourceBurning;
    private AudioSource audioSourceExploding;

    private void Start()
    {
        // Health system
        health = maxHealth;

        HomingSFX = transform.GetChild(0).gameObject;
        BurningSFX = transform.GetChild(1).gameObject;
        ExplodingSFX = transform.GetChild(2).gameObject;

        audSourceHoming = HomingSFX.GetComponent<AudioSource>();
        audioSourceBurning = BurningSFX.GetComponent<AudioSource>();
        audioSourceExploding = ExplodingSFX.GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player GameObject not found! Make sure the player has the 'Player' tag.");
        }

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            // Calculate the movement vector
            Vector2 moveDirection = direction * speed * Time.deltaTime;

            if (distance < distanceBetween)
            {
                // Move the goblin towards the player
                audSourceHoming.Play();
                transform.Translate(moveDirection);
                animator.SetBool("isMoving", true);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            // Update the blend tree parameters
            animator.SetFloat("horizontalMovement", moveDirection.x);
            animator.SetFloat("verticalMovement", moveDirection.y);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // 3 -> 2 -> 1 -> 0 = Enemy has died

        if (health <= 0)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the collision is with the player
            // Call the Die() function of the player when hit by the fireball
            collision.gameObject.GetComponent<PlayerController>().Die();
            audioSourceExploding.Play();
        }
    }

    private void Die()
    {
        // Stop following the player when the enemy dies
        isFollowingPlayer = false;

        // Debug
        Debug.Log("Enemy Fireball died!");

        // Disable the BoxCollider component to prevent further collisions
        GetComponent<BoxCollider2D>().enabled = false;

        // Play the death animation by setting the "isDead" parameter to true
        audioSourceBurning.Play();
        animator.SetBool("isDead", true);

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 1.2f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);

        // Trigger the scoreCount event when the goblin dies
        ScoreCounter.Instance.IncreaseScore(value);
    }



}
