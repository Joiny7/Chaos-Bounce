using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisAudioScript : MonoBehaviour {

    public AudioSource source;
    public AudioClip hit;
    private float pitch;
    private MainController main;


    private void Awake()
    {
        pitch = 0.6f;
        main = FindObjectOfType<MainController>();
    }

    public void PlayHit()
    {
        //if (main.Sound)
        //{
            source.pitch = pitch;
            source.volume = 0.4f;
            source.PlayOneShot(hit);
        //}
    }
}