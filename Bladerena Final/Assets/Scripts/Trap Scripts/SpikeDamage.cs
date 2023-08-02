using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamage : Damage
{
     
    private BoxCollider2D boxCol2d;

    // Start is called before the first frame update
    void Start()
    {
        
        boxCol2d = GetComponent<BoxCollider2D>();
    }

    public void ShowCollider() {

        // Play SFX
        AudioManager.Instance.PlaySFX("spike");
        boxCol2d.enabled = true;
    }

    public void HideCollider()
    {
        boxCol2d.enabled = false;

    }



}
