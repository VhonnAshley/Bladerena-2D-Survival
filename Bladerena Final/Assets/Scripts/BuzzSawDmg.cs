using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzSawDmg : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>().Damage(1);
    }
}
