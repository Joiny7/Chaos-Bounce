using UnityEngine;

public class Plasma : BaseObject {

    private PowerUpsController powerUpsController;

    private void Awake()
    {
        powerUpsController = FindObjectOfType<PowerUpsController> ();
    }

    private void OnDestroy()
    {
        FindObjectOfType<Spawner>().powerUps.Remove(gameObject);
        FindObjectOfType<Spawner> ().BlockDestroyed ();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            powerUpsController.PickUpPlasma();
            FindObjectOfType<StatusController>().UpdateHistory("Plasma added to inventory", Color.magenta);
            Destroy(gameObject);
        }
    }
}