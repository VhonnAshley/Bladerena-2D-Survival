using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordlierController : MonoBehaviour
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

    private void Start()
    {
        // Health system
        health = maxHealth;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFollowingPlayer)
        {
            // Enemy health
            // [SerializeField] float health, maxHealth = 3f; // Remove this line, it should not be here

            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            // Calculate the movement vector
            Vector2 moveDirection = direction * speed * Time.deltaTime;

            if (distance < distanceBetween)
            {
                // Move the goblin towards the player
                transform.Translate(moveDirection);
                animator.SetBool("isMoving", true);
            }
            if (distance > distanceBetween)
            {
                animator.SetBool("isMoving", false);
            }

            // Update the blend tree parameters
            animator.SetFloat("horizontalMovement", moveDirection.x);
            animator.SetFloat("verticalMovement", moveDirection.y);
        }
    }

<<<<<<< HEAD
    private void UpdateAnimator(Vector2 moveDirection)
    {
        // Update the blend tree parameters
        animator.SetFloat("horizontalMovement", moveDirection.x);
        animator.SetFloat("verticalMovement", moveDirection.y);

        // Determine if the enemy is moving or idle
        bool isMoving = moveDirection.magnitude > 0.1f;
    
        animator.SetBool("isMoving", true);
    }

=======
>>>>>>> f02a4baea92374f94cf583ff5691f35975103b1c
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // 3 -> 2 -> 1 -> 0 = Enemy has died

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Stop following the player when the enemy dies
        isFollowingPlayer = false;

        // Debug
        Debug.Log("Enemy Swordlier died!");

        // Disable the Collider component to prevent further collisions
        GetComponent<PolygonCollider2D>().enabled = false;

        // Play the death animation by setting the "isDead" parameter to true
        animator.SetBool("isDead", true);

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 2f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);
    }
}
