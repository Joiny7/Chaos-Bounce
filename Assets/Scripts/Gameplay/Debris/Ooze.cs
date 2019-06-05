using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ooze : BaseObject {

    private Vector3 sizeChange = new Vector3(0.2f, 0.2f, 0);
    private EffectController effects;
    private OrbLauncher launcher;
    private GameSoundController gameSound;
    private int orbsRemoved = 0;
    public Color textColour;

    Spawner spawner;

    private void Awake()
    {
        effects = FindObjectOfType<EffectController>();
        launcher = FindObjectOfType<OrbLauncher>();
        gameSound = FindObjectOfType<GameSoundController>();

        if(spawner == null) {
            spawner = FindObjectOfType<Spawner> ();
        }
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

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            if (col.GetComponent<Orb>().turnedToPlasma)
            {
                FindObjectOfType<EffectController>().PlasmaDeath(gameObject.transform.position);
                Destroy(gameObject);
            }
            else
            {
                RemoveOrbFromGame(col.gameObject);
                FindObjectOfType<StatusController>().UpdateHistory("Abducted!", textColour);
            }
        }
    }

    private void DecreaseSize()
    {
        if (transform.localScale.x < 0.1f)
        {
            Destroy(gameObject);
        }

        gameSound.OozeEffect();
        transform.localScale -= sizeChange;
    }

    private void RemoveOrbFromGame(GameObject obj)
    {
        effects.Dissolve(obj.transform.position);
        FindObjectOfType<OrbLauncher>().orbs.Remove(obj.GetComponent<Orb>());
        orbsRemoved++;

        if (orbsRemoved == 10)
            Destroy(gameObject);

        if (FindObjectOfType<OrbLauncher>().orbs.Count == 0)
        {
            var effects = FindObjectOfType<EffectController>();
            effects.StartGameOver();
        }

        FindObjectOfType<StatusController>().UpdateHistory("Orbs lost: " + orbsRemoved.ToString());
        Destroy(obj);
    }
}