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
}