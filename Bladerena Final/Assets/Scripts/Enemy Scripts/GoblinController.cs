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
        // Check the distance to the player and determine if the enemy should follow or not
        distance = Vector2.Distance(transform.position, player.transform.position);
        isFollowingPlayer = distance < distanceBetween;

        if (isFollowingPlayer)
        {
            Vector2 direction = player.transform.position - transform.position;
            direction.Normalize();

            // Calculate the movement vector
            Vector2 moveDirection = direction * speed * Time.deltaTime;

            // Move the goblin towards the player
            GetComponent<Rigidbody2D>().velocity = moveDirection;

            // Update the blend tree parameters
            UpdateAnimator(moveDirection);
        }
        else
        {
            // Stop the goblin if the player is too far away
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            // Update the blend tree parameters with a zero moveDirection
            UpdateAnimator(Vector2.zero);
        }
    }

    private void UpdateAnimator(Vector2 moveDirection)
    {
        // Update the blend tree parameters
        animator.SetFloat("horizontalMovement", moveDirection.x);
        animator.SetFloat("verticalMovement", moveDirection.y);

        // Determine if the enemy is moving or idle
        bool isMoving = moveDirection.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);
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

        // Debug
        Debug.Log("Enemy Goblin died!");

        // Play the death animation by setting the "isDead" parameter to true
        animator.SetBool("isDead", true);

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 2f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);
    }
}