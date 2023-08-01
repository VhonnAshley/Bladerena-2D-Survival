using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private const float SPEED = 7f;
    private const float ATTACK_ANIMATION_DURATION = 0.5f; // Replace with the actual duration of your attack animation

    [SerializeField] private LayerMask dashLayerMask;

    private new Rigidbody2D rigidbody2D;
    private Vector2 moveDir;
    private Vector2 lastMoveDir;
    private Animator animator;
    private bool isDashButtonDown;
    private bool isAttacking; // New variable to track the attack animation state

    // When player death animation plays, disable user input
    private bool isPlayerAlive = true; // New variable to track player's life state

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isPlayerAlive)
        {
            HandleInput();
            HandleMovement();
        }

        // Check for left mouse button click and trigger the attack animation
     /*   if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            animator.SetBool("isAttacking", true);
            StartCoroutine(ResetAttackAnimation());
        }*/
    }

    private void HandleInput()
    {
        if (!isPlayerAlive)
        {
            // If the player is dead, don't handle any input
            return;
        }

        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }

        moveDir = new Vector2(moveX, moveY).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDashButtonDown = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            animator.SetBool("isDashing", false);
        }
    }

    private void HandleMovement()
    {
        bool isIdle = moveDir.x == 0 && moveDir.y == 0;
        if (isIdle)
        {
            // Idle
            rigidbody2D.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
        else
        {
            // Moving
            lastMoveDir = moveDir;
            rigidbody2D.velocity = moveDir * SPEED;
            animator.SetFloat("horizontalMovement", moveDir.x);
            animator.SetFloat("verticalMovement", moveDir.y);
            animator.SetBool("isMoving", true);
        }
    }

    private void FixedUpdate()
    {
        if (isDashButtonDown)
        {
            float dashDistance = 5f;
            Vector2 dashPosition = (Vector2)transform.position + moveDir * dashDistance;

            // Use Continuous collision detection to prevent getting stuck inside colliders
            rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDir, dashDistance, dashLayerMask);

            if (hit.collider != null)
            {
                dashPosition = hit.point - moveDir * 0.5f; // Move slightly away from the collider to avoid getting stuck
            }

            rigidbody2D.MovePosition(dashPosition);
            rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete; // Set back to Discrete after dashing

            // Trigger the dash animation by setting the "isDashing" parameter to true
            animator.SetBool("isDashing", true);

            isDashButtonDown = false;
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Check if the player collided with the enemy
            // You can add damage handling here if you want

            // Call the Die() function to initiate the death process
            Die();
        }
    }
    private void Die()
    {
        // Debug
        Debug.Log("Player has died!");

        // Play the death animation by setting the "isDead" parameter to true
        animator.SetBool("isDead", true);

        // Set the isPlayerAlive flag to false when the player dies
        isPlayerAlive = false;

        // Freeze the player's movement by setting Rigidbody2D constraints
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        // Disable the BoxCollider component to prevent further collisions
        GetComponent<BoxCollider2D>().enabled = false;

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 3f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);
    }


    /* private IEnumerator ResetAttackAnimation()
     {
         // Set the isAttacking flag to true while the animation is playing
         isAttacking = true;

         // Wait for the attack animation to finish playing
         yield return new WaitForSeconds(ATTACK_ANIMATION_DURATION);

         // Set the "isAttacking" parameter back to false
         animator.SetBool("isAttacking", false);

         // Set the isAttacking flag to false when the animation is done
         isAttacking = false;
     }*/

}
