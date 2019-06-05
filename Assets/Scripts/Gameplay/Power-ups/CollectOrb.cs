using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrb : BaseObject
{
    public Color textColour;
    private Spawner spawner;
    private OrbLauncher launcher;
    private float rotSpeed = 45f;
    [SerializeField]
    private DebrisOrb dOrbPrefab;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        launcher = FindObjectOfType<OrbLauncher>();
    }

    private void OnDestroy()
    {
        spawner.orbsSpawned.Remove(gameObject);
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        launcher.CreateOrb();
        SpawnDebrisOrb();
        FindObjectOfType<GameSoundController>().PlayCollectOrbEffect();
        FindObjectOfType<StatusController>().UpdateHistory("Ammo picked up", textColour);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Rotate(Vector3.back, rotSpeed * Time.deltaTime);
    }

    private void SpawnDebrisOrb()
    {
        var dorb = Instantiate(dOrbPrefab, gameObject.transform.position, Quaternion.identity);
    }
}