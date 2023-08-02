using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;

    private Rigidbody2D rb;
    private Animator anim;

    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerController>();

        currentHealth = maxHealth;
    }

    public void Damage(int dmgAmount) {

        currentHealth = currentHealth - dmgAmount;

        //Player Gonna die;
        if (currentHealth <= 0)
        {
            pc.Die();

        }

    }

    /*private void OnCollisionEnter2D(Collision2D collision)
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
        anim.SetBool("isDead", true);

        // Destroy the enemy GameObject after some time (adjust the delay as needed)
        float deathAnimationDuration = 3f; // Replace with the actual duration of the death animation
        Destroy(gameObject, deathAnimationDuration);
    }*/





}
