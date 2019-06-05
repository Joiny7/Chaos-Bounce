using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grower : BaseObject {

    private Spawner spawner;
    private OrbLauncher launcher;
    private GameSoundController gameSound;

    private void Awake ()
    {
        launcher = FindObjectOfType<OrbLauncher>();
        spawner = FindObjectOfType<Spawner>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            GrowBalls();
            gameSound.GrowerEffect();
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        FindObjectOfType<Spawner> ();
    }

    private void GrowBalls() 
    {
        var orbs = launcher.orbs;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];
            orb.transform.localScale += new Vector3(0.035f, 0.035f, 0);
        }
        FindObjectOfType<StatusController>().UpdateHistory("Shot size doubled");
    }
}