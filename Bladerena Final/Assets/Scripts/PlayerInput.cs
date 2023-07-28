using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]public float moveSpeed = 5f; // Adjust the player's movement speed in the Inspector.
    private bool facingRight = true; // Track the player's facing direction.

    private Animator myAnim;
    private Rigidbody2D rb;

    void Start()
    {
        myAnim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Animation handling
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            myAnim.SetInteger("State", 1);
            FlipCharacter(true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            myAnim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            myAnim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            myAnim.SetInteger("State", 3);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myAnim.SetInteger("State", 4);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            myAnim.SetInteger("State", 5);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            myAnim.SetInteger("State", 1);
            FlipCharacter(false);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            myAnim.SetInteger("State", 0);
        }

        // Player Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);
        movement.Normalize();

        rb.velocity = movement * moveSpeed;
    }

    void FlipCharacter(bool faceRight)
    {
        if (faceRight && !facingRight || !faceRight && facingRight)
        {
            facingRight = !facingRight;

            Vector3 characterScale = transform.localScale;
            characterScale.x *= -1;
            transform.localScale = characterScale;
        }
    }
}
