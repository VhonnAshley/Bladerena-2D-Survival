using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordlier : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float distanceBetween;

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

        if (distance < distanceBetween)
        {
            // Move the goblin towards the player
            transform.Translate(moveDirection);
        }

        // Update the blend tree parameters
        animator.SetFloat("horizontalMovement", moveDirection.x);
        animator.SetFloat("verticalMovement", moveDirection.y);
    }
}
