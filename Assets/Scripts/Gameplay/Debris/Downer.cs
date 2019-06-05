using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downer : BaseObject {

    private Spawner spawner;
    private EffectController effects;
    private GameSoundController gameSound;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        effects = FindObjectOfType<EffectController>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private bool isQuitting = false;

    void OnApplicationQuit () {
        isQuitting = true;
    }

    private void OnDestroy () {
        if (!isQuitting) {
            Instantiate (Resources.Load ("DestroyEffect/" + System.Enum.GetName (typeof (DamageReductionState), state), typeof (GameObject)), transform.position, transform.rotation);
        }
        spawner.allDebris.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Debris"))
        {
            Destroy(coll.gameObject);
        }
        if (coll.CompareTag("Ball"))
        {
            if (coll.gameObject.GetComponent<Orb>().turnedToPlasma)
            {
                effects.PlasmaDeath(gameObject.transform.position);
                Destroy(gameObject);
            }
            else if (coll.gameObject.GetComponent<Orb>().turnedToFire)
            {
                effects.SmallSpark(gameObject.transform.position);
                return;
            }
            else
            {
                gameSound.DownerEffect();
                coll.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                coll.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.2f;
            }
        }
    }
}