using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusiCTRL : MonoBehaviour
{
    [SerializeField] AudioSource music;

    public void onMusic()
    {
        music.Play();
    }
    public void offMusic()
    {
        music.Stop();
    }
}
