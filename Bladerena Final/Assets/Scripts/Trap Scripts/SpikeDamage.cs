using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : Damage
{
     
    private BoxCollider2D boxCol2d;
    private AudioSource audSource;

    // Start is called before the first frame update
    void Start()
    {
        
        boxCol2d = GetComponent<BoxCollider2D>();
        audSource = GetComponent<AudioSource>();
    }

    public void ShowCollider() {

        audSource.Play();
        boxCol2d.enabled = true;
    }

    public void HideCollider()
    {
        boxCol2d.enabled = false;

    }



}
