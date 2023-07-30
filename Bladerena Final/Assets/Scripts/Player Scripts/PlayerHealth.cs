using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;

    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;

    }

    public void Damage(int dmgAmount) {

        currentHealth = currentHealth - dmgAmount;

        //rb.bodyType = RigidbodyType2D.Static;
        //Play Death Animation

        if (currentHealth <= 0)
        {
            Debug.Log("Player Hit");
            anim.SetBool("isDead", true);
            //rb.bodyType = RigidbodyType2D.Static;


        }

    }



}
