using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : BaseObject {

    private float rotSpeed = 45f;
    private Spawner spawner;
    private PowerUpsController powerUpsController;

    private void Awake ()
    {
        spawner = FindObjectOfType<Spawner> ();
        hitsRemaining = 1;
        powerUpsController = FindObjectOfType<PowerUpsController> ();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    void Update ()
    {
        transform.Rotate(Vector3.back, rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            powerUpsController.PickUpTripleShot();
            FindObjectOfType<StatusController>().UpdateHistory("Triple shot added to inventory");
            Destroy(gameObject);
        }
    }
}