using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Transform playerTransform;

    private Animator animator;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            // Get the direction from the enemy to the player
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            directionToPlayer.Normalize();

            // Set the move direction for the blend tree
            float moveX = directionToPlayer.x;
            float moveY = directionToPlayer.y;

            // Update the blend tree parameters
            animator.SetFloat("horizontalMovement", moveX);
            animator.SetFloat("verticalMovement", moveY);

            // Calculate the movement vector
            Vector2 moveDirection = new Vector2(moveX, moveY);

            // Move the enemy towards the player
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
