using System.Collections.Generic;
using UnityEngine;

public class MapKiller : BaseObject {

    private Spawner spawner;
    private PowerUpsController powerUpsController;

    private void Awake () {
        powerUpsController = FindObjectOfType<PowerUpsController> ();
        spawner = FindObjectOfType<Spawner> ();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            powerUpsController.PickUpMapKiller ();
            FindObjectOfType<StatusController>().UpdateHistory("Nuke added to inventory", Color.green);
            Destroy (gameObject);
        }
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }
}