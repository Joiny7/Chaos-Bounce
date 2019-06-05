using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrinker : BaseObject {

    private Spawner spawner;
    private OrbLauncher launcher;
    private GameSoundController gameSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            ShrinkBalls();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void ShrinkBalls()
    {
        launcher = FindObjectOfType<OrbLauncher>();
        var orbs = launcher.orbs;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];
            orb.transform.localScale -= new Vector3(0.035f, 0.035f, 0);

            if (orb.transform.localScale.x < 0.018f)
            {
                orb.transform.localScale = new Vector3(0.018f, 0.018f, 0);
            }
        }

        gameSound.ShrinkerEffect();
        FindObjectOfType<StatusController>().UpdateHistory("Size decreased by half");
    }
}