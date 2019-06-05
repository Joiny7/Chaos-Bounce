using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrenade : BaseObject {

    private EffectController effects;
    private GameSoundController gameSound;
    private Spawner spawner;
    private float distanceBetweenObjects;
    private List<GameObject> ToDie;

    private void Awake()
    {
        gameSound = FindObjectOfType<GameSoundController>();
        effects = FindObjectOfType<EffectController>();
        spawner = FindObjectOfType<Spawner>();
        ToDie = new List<GameObject>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            Explode();
            FindObjectOfType<StatusController>().UpdateHistory("Grenade detonated!");
        }
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    public void Explode()
    {
        distanceBetweenObjects = spawner.distanceBetweenObjects;
        CheckForHits();

        foreach (GameObject g in ToDie)
        {
            if (g.GetComponent<Wall>())
            {
                break;
            }
            else if (g.GetComponent<Orb>())
            {
                break;
            }
            else if (g.GetComponent<BlackOut>())
            {
                g.GetComponent<BlackOut>().StopMe();
                Destroy(g);
                break;
            }
            else if (g.GetComponent<Pentagram>())
            {
                g.GetComponent<Pentagram>().hitsRemaining -= 100;
                break;
            }
            else if (g.GetComponent<Ooze>())
            {
                break;
            }
            else
            {
                effects.SmallBoom(g.transform.position);
                Destroy(g);
            }
        }

        effects.SmallBoom(gameObject.transform.position);
        gameSound.LineKillerEffect();
        Destroy(gameObject);
    }

    private void CheckForHits()
    {
        var spawnedObjects = FindObjectOfType<SpawnHelper>().spawnedObjectsParent;

        for (int i = 0; i < spawnedObjects.childCount; i++)
        {
            GameObject G = spawnedObjects.GetChild(i).gameObject;

            if ((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects * distanceBetweenObjects * 1.1f)
            {
                ToDie.Add(G);
            }
        }
    }
}