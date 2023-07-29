using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCol2d;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerHealth>().Damage(1);
    }

    public void ShowCollider() {
        boxCol2d.enabled = true;
    }

    public void HideCollider()
    {
        boxCol2d.enabled = false;

    }



}
