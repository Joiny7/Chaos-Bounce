using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleUseBlockSoundScript : MonoBehaviour {

    public AudioSource source;
    private bool played;
    public int priorityChange;

    private void Update()
    {
        ChangePriorityOverTime();

        if (!source.isPlaying && played)
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip sound, float pitch)
    {
        source.pitch = pitch;
        source.PlayOneShot(sound);
        played = true;
    }

    private void ChangePriorityOverTime()
    {
        source.priority += priorityChange;
    }
}