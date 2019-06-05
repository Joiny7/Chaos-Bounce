using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricPickup : MonoBehaviour {

    private GameSoundController gameSound;
    private Spawner spawner;
    private PowerUpsController powerControl;
    private EffectController effects;

    private void Awake()
    {
        gameSound = FindObjectOfType<GameSoundController>();
        spawner = FindObjectOfType<Spawner>();
        powerControl = FindObjectOfType<PowerUpsController>();
        effects = FindObjectOfType<EffectController>();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        FindObjectOfType<Spawner> ();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            powerControl.PickUpElectricity();
            FindObjectOfType<StatusController>().UpdateHistory("Electricity added to inventory", Color.cyan);
            Destroy(gameObject);
        }
    }
}