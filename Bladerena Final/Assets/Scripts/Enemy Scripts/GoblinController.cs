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

    private void Start()
    {
        // Health system
        health = maxHealth;

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        // Calculate the movement vector
        Vector2 moveDirection = direction * speed * Time.deltaTime;

        if (distance < distanceBetween)
        {
            // Move the goblin towards the player
            transform.Translate(moveDirection);
        }

        // Update the blend tree parameters
        animator.SetFloat("horizontalMovement", moveDirection.x);
        animator.SetFloat("verticalMovement", moveDirection.y);
    }

    /*public void TakeDamage(float damageAmount)
    {
        health -= damageAmount; // 3 -> 2 -> 1 -> 0 = Enemy has died

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }*/

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
        float deathAnimationDuration = 1f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);
    }
}
