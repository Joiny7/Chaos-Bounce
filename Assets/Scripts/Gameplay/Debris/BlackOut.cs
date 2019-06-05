using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOut : BaseObject {

    private Camera cam;
    private Spawner spawner;
    private MainController main;
    public LayerMask darknessOn;
    public LayerMask darknessOff;
    private GameSoundController gameSound;
    public Color textColour;

    private void OnEnable()
    {
        cam = FindObjectOfType<Camera>();
        spawner = FindObjectOfType<Spawner>();
        main = FindObjectOfType<MainController>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private void Start()
    {
        StartMe();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Ball"))
        {
            StopMe();
            Destroy(gameObject);
        }
    }

    private bool isQuitting = false;

    void OnApplicationQuit () {
        isQuitting = true;
    }

    private void OnDestroy () {
        if (!isQuitting) {
            Instantiate (Resources.Load ("DestroyEffect/" + System.Enum.GetName (typeof (DamageReductionState), state), typeof (GameObject)), transform.position, transform.rotation);
        }
        spawner.allDebris.Remove(gameObject);
        spawner.BlockDestroyed ();

        StopMe ();
    }

    private void StartMe()
    {
        FindObjectOfType<StatusController>().UpdateHistory("Random effect, Vision Eater!", textColour);
        cam.backgroundColor = Color.black;
        cam.cullingMask = darknessOn;
        main.BlackOUT = true;
        gameSound.BlackOutOnEffect();
    }

    public void StopMe()
    {
        cam.cullingMask = darknessOff;
        main.BlackOUT = false;
        gameSound.BlackOutOffEffect();
    }
}