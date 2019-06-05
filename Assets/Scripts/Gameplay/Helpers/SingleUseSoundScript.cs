using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUseSoundScript : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;
    public float volume;

    private void Awake()
    {
        Play();
    }

    private void Update()
    {
        if (!source.isPlaying)
            Destroy(gameObject);
    }

    private void Play()
    {
        source.volume = volume;
        source.PlayOneShot(clip);
    }
}