using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : BaseObject {

    private Animator camerAnimator;
    private MainController main;
    private Spawner spawner;
    private GameSoundController gameSound;

    private void Awake ()
    {
        camerAnimator = FindObjectOfType<Camera>().GetComponent<Animator>();
        main = FindObjectOfType<MainController>();
        gameSound = FindObjectOfType<GameSoundController>();
        spawner = FindObjectOfType<Spawner>();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Ball")) {
            FindObjectOfType<StatusController>().UpdateHistory("Field rotated for 1 level");

            camerAnimator.SetBool("Rotate", true);
            camerAnimator.SetBool("RotateBack", false);

            main.Rotated = true;

            gameSound.RotatorEffect();

            Destroy(gameObject);
        }
    }
}