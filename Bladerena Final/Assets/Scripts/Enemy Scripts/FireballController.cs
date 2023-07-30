using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public GameObject player;
    public float speed;

    private float distance;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Calculate the movement vector
        Vector2 moveDirection = direction * speed * Time.deltaTime;

        // Move the Fireball towards the player
        transform.Translate(moveDirection);

        // Set the isMoving parameter in the animator based on the magnitude of moveDirection
        animator.SetBool("isMoving", moveDirection.magnitude > 0f);

        // Update the blend tree parameter for horizontal movement
        animator.SetFloat("horizontalMovement", moveDirection.x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player collided with the enemy
            // You can add damage handling here if you want

            // Call the Die() function to initiate the death process
            Die();
        }
    }

    private void Die()
    {
        // Play the death animation by setting the "isDead" parameter to true
        animator.SetBool("isDead", true);

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 1.2f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);
    }
}
