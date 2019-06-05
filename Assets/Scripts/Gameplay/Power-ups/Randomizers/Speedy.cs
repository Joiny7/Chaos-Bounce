using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speedy : BaseObject {

    private OrbLauncher launcher;
    private Spawner spawner;
    private GameSoundController gameSound;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        launcher = FindObjectOfType<OrbLauncher>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            SpeedUp();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void SpeedUp()
    {
        var orbs = launcher.orbs;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];

            if (orb != null)
            {
                orb.moveSpeed += orb.moveSpeed;
            }         
        }

        gameSound.SpeedyEffect();
    }
}