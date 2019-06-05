using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slowy : BaseObject {

    private OrbLauncher launcher;
    private Spawner spawner;
    private GameSoundController gameSound;
    public Color textColour;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            SlowDown();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        launcher = FindObjectOfType<OrbLauncher>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void SlowDown()
    {
        var orbs = launcher.orbs;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];

            if (orb != null)
            {
                orb.SlowDown ();
            }
        }

        gameSound.SlowyEffect();
        FindObjectOfType<StatusController>().UpdateHistory("Speed decreased by half", textColour);
    }
}