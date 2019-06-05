using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAudioScript : MonoBehaviour {

    public AudioClip[] hits;
    public float pitch;
    private int startHits;
    private GameSoundController gameSound;
    public GameObject singleUsePrefab;

    private void OnDestroy()
    {
        gameSound.DestroyBlock();
    }

    private void Awake()
    {
        var test1 = FindObjectOfType<GameSoundController>().blockSounds;
        gameSound = FindObjectOfType<GameSoundController>();
        hits = test1;
    }

    public void Calculate(int currentHits)
    {
        float hitPercent = ((float)currentHits / (float)startHits) * 3f;
        pitch = hitPercent;
    }

    public void StartPrep(int hits)
    {
        startHits = hits;
    }

    public void PlayHit(int currentHits)
    {
        Calculate(currentHits);
        int soundNumber = UnityEngine.Random.Range(0, 7);
        AudioClip sound = hits[soundNumber];
        var soundPref = Instantiate(singleUsePrefab);
        soundPref.transform.SetParent(gameSound.soundParent.transform);
        soundPref.GetComponent<SingleUseBlockSoundScript>().priorityChange = 1;
        soundPref.GetComponent<SingleUseBlockSoundScript>().PlaySound(sound, pitch);
    }    
}