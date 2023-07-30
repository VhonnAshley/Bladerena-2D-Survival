using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int dmgAmount) {

        currentHealth = currentHealth - dmgAmount;

        //rb.bodyType = RigidbodyType2D.Static;
        //Play Death Animation

        Debug.Log("Player Hit");
        //Destroy(gameObject);
        //Show Game Over Screen

   
    }
}
