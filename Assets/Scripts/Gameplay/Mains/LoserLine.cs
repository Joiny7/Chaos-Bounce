using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoserLine : MonoBehaviour {

    [SerializeField]
    private EffectController effects;
    private MainController main;
    public UploadHighscore uploader;
    private bool triedToUpload = false;

    public delegate void LoserLineAction ();
    public static event LoserLineAction GameOver;

    private void Awake () {
        transform.localScale *= Camera.main.aspect / (9.0f / 16.0f);
        main = FindObjectOfType<MainController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (triedToUpload == false)
            {
                UploadHighscore();
            }
            
            effects.StartGameOver();
            GameOver ();
        }
        if (col.CompareTag("Debris") || col.CompareTag("Power-up"))
        {
            if (col.gameObject.GetComponent<BlackOut>())
            {
                col.gameObject.GetComponent<BlackOut>().StopMe();
            }
            if (!col.GetComponent<DebrisOrb>())
            {
                effects.BurnInAtmosphereEffect(col.transform.position);
            }
            Destroy(col.gameObject);
        }
        if (col.CompareTag("Wall"))
        {
            effects.BurnInAtmosphereEffect(col.transform.position);
            Destroy(col.gameObject);
        }
    }

    private void UploadHighscore()
    {
        triedToUpload = true;
        StartCoroutine(uploader.GetMyScore());

        if (main.highScore > uploader.existingScore)
        {
            PlayerData data = SaveSystem.LoadPlayer ();
            if (data.userId == null) {
                StartCoroutine (uploader.PostHighScore (data.playerName, main.CurrentScore));
            } else {
                StartCoroutine (uploader.UpdateHighscore (data.userId, data.playerName, main.CurrentScore));
            }
        }
    }
}