using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusController : MonoBehaviour {

    public TextMeshProUGUI score;
    public TextMeshProUGUI shots;
    public TextMeshProUGUI recentHistory;
    private int shotsReady;
    private float historyTimer;
    private bool newUpdate;
    private OrbLauncher launcher;

	void Start ()
    {
        score.text = FindObjectOfType<Spawner>().rowsSpawned.ToString();
        launcher = FindObjectOfType<OrbLauncher>();
        SetShots(launcher.orbs.Count);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (newUpdate)
        {
            historyTimer += Time.deltaTime;

            if (historyTimer > 5)
            {
                newUpdate = false;
                historyTimer = 0;
                recentHistory.text = "";
            }
        }
	}

    public void SetShots(int number)
    {
        if (number != 0)
        {
            shotsReady = number;
            shots.text = shotsReady.ToString();
        }
        else
        {
            shots.text = "Reloading";
        }
    }

    public void SetShotNewRound()
    {
        if(launcher == null) {
            launcher = FindObjectOfType<OrbLauncher> ();
        }

        shots.text = launcher.orbs.Count.ToString();
        shotsReady = launcher.orbsReady;
    }

    public void DecreaseShots()
    {
        shotsReady--;

        if (shotsReady > 0)
        {
            shots.text = shotsReady.ToString();
        }
        else
        {
            shots.text = "Reloading";
        }
    }

    public void UpdateHistory(string update)
    {
        recentHistory.color = Color.white;
        recentHistory.text = update;
        historyTimer = 0;
        newUpdate = true;
    }

    public void UpdateHistory(string update, Color col)
    {
        recentHistory.color = col;
        recentHistory.text = update;
        historyTimer = 0;
        newUpdate = true;
    }
}