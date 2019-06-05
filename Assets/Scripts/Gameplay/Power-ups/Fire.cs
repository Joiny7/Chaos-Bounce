using UnityEngine;

public class Fire : BaseObject {

    private Spawner spawner;
    private PowerUpsController powerUpsController;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        powerUpsController = FindObjectOfType<PowerUpsController>();
    }

    private void OnDestroy()
    {
        if (spawner)
        {
            spawner.powerUps.Remove(gameObject);
            spawner.BlockDestroyed ();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            powerUpsController.PickUpFire();
            FindObjectOfType<StatusController>().UpdateHistory("Fire added to inventory", Color.red);
            Destroy(gameObject);
        }
    }
}