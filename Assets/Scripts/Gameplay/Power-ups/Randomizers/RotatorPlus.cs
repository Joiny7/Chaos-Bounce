using UnityEngine;

public class RotatorPlus : BaseObject {

    private Animator camerAnimator;
    private MainController main;
    private Spawner spawner;
    private GameSoundController gameSound;

    private void Awake ()
    {
        camerAnimator = FindObjectOfType<Camera>().GetComponent<Animator> ();
        main = FindObjectOfType<MainController>();
        spawner = FindObjectOfType<Spawner>();
        gameSound = FindObjectOfType<GameSoundController>();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag ("Ball")) {
            FindObjectOfType<StatusController>().UpdateHistory("Field rotated until next RotatorPlus");
            camerAnimator.SetTrigger("RotatorPlus");
            camerAnimator.SetBool("RotateBack", false);
            main.Rotated = false;
            gameSound.RotatorPlusEffect();
            Destroy (gameObject);
        }
    }
}