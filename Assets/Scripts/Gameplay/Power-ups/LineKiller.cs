using System.Collections.Generic;
using UnityEngine;

public class LineKiller : BaseObject {

    private Spawner spawner;
    private EffectController effects;
    private GameSoundController gameSound;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ball")
        {
            FindObjectOfType<StatusController>().UpdateHistory("Row destroyed!");
            KillLine();
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        gameSound = FindObjectOfType<GameSoundController>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void KillLine()
    {
        spawner = FindObjectOfType<Spawner>();
        effects = FindObjectOfType<EffectController>();
        List<GameObject> allObjects = new List<GameObject>();
        allObjects.AddRange(spawner.allEnemies);
        allObjects.AddRange(spawner.allDebris);
        allObjects.AddRange(spawner.powerUps);
        allObjects.AddRange(spawner.orbsSpawned);
        List<GameObject> affected = new List<GameObject>();

        for (int i = 0; i < allObjects.Count; i++)
        {
            var obj = allObjects[i];

            if (obj != null)
            {
                if (obj.transform.position.y == transform.position.y)
                {
                    affected.Add(obj);
                }
            }
        }

        for(int y = 0; y < affected.Count; y++)
        {
            var affectedObj = affected[y];

            if (affectedObj != null)
            {
                if (affectedObj.GetComponent<BlackOut>())
                {
                    affectedObj.GetComponent<BlackOut>().StopMe();
                }
                if (affectedObj.GetComponent<Pentagram>())
                {
                    affectedObj.GetComponent<Pentagram>().hitsRemaining -= 100;
                    return;
                }
                if (affectedObj.GetComponent<Ooze>())
                {
                    return;
                }

                effects.SmallBoom(affectedObj.transform.position);
                Destroy(affectedObj);
            }          
        }

        gameSound.LineKillerEffect();
    }
}