using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniWall : BaseObject {

    private Spawner spawner;
    private DebrisAudioScript audioPlayer;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        audioPlayer = GetComponentInChildren<DebrisAudioScript>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            if (col.gameObject.GetComponent<Orb>().turnedToPlasma)
            {
                FindObjectOfType<EffectController>().PlasmaDeath(gameObject.transform.position);
                Destroy(gameObject);
            }
            else
            {
                audioPlayer.PlayHit();
            }
        }
    }

    private void OnDestroy()
    {
        if (spawner)
        {
            spawner.allDebris.Remove(gameObject);
            spawner.BlockDestroyed ();
        }
    }
}