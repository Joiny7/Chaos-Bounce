using System;
using UnityEngine;
using UnityEngine.UI;

public class AutoPlayer : MonoBehaviour
{
    public string target;
    public float timeScale = 5f;

    public bool changeRound = false;
    public int startRound = 1;
    public int newOrbsCount = 1;

    private OrbLauncher orbLauncher;

    public Type type;

    void OnEnable()
    {
        orbLauncher = FindObjectOfType<OrbLauncher>();
        Time.timeScale = timeScale;
    }

    void Update()
    {
        if (changeRound)
        {
            Spawner spawner = FindObjectOfType<Spawner>();

            int orbsToSpawn = newOrbsCount - orbLauncher.orbsReady;

            for (int i = 0; i < orbsToSpawn; i++)
            {
                orbLauncher.CreateOrb();
            }

            spawner.rowsSpawned = startRound;
            changeRound = false;
        }

        if (orbLauncher.orbsReady == orbLauncher.orbs.Count && !changeRound) {
            BaseObject[] baseObjects = FindObjectsOfType<BaseObject> ();

            for (int i = 0; i < baseObjects.Length; i++) {
                if (baseObjects[i].name.Equals (target + "(Clone)")) {
                    enabled = false;
                    Time.timeScale = 1f;
                    return;
                }
            }

            orbLauncher.AutoLaunch();
        }
    }
}
