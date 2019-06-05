using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FlippyFloop : BaseObject {

    private Spawner spawner;
    public GameObject BlackOut;
    public GameObject Downer;
    public GameObject Abductor;
    public string Effect = "";
    private bool inChild = false;
    public Color[] textColours;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        ChooseEffect();
        Setup();
    }

    private void OnDestroy()
    {
        spawner.powerUps.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    private void Update()
    {
        //Debug.Log(Effect);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !inChild)
        {
            Flippy();
        }
        if (inChild)
        {
            if (Effect.Equals("blackout"))
            {
                Destroy(gameObject);
            }
            if (Effect.Equals("downer"))
            {
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Downer!", textColours[2]);
            }
            if (Effect.Equals("abductor"))
            {
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Abductor!", textColours[2]);
            }
        }
    }

    private void Setup()
    {
        if (Effect.Equals("blackout"))
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            inChild = true;
            BlackOut.SetActive(true);
            FindObjectOfType<StatusController>().UpdateHistory("Random effect, Vision Eater!", textColours[2]);
        }
        else if (Effect.Equals("downer"))
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            inChild = true;
            Downer.SetActive(true);
        }
        else if (Effect.Equals("abductor"))
        {
            //GetComponent<BoxCollider2D>().enabled = false;
            inChild = true;
            Abductor.SetActive(true);
        }
    }

    private void Flippy()
    {
        spawner.FlippyFloopPicked ();

        switch (Effect)
        {
            case "ice":
                //do ice
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Frozen!", textColours[0]);
                Destroy(gameObject);
                break;
            case "metal":
                //do metal
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Metal coated!", Color.grey);
                Destroy(gameObject);
                break;
            case "grower":
                GrowBalls();
                Destroy(gameObject);
                break;
            case "rotator":
                var camerAnimator = FindObjectOfType<Camera>().GetComponent<Animator>();
                camerAnimator.SetBool("Rotate", true);
                camerAnimator.SetBool("RotateBack", false);
                FindObjectOfType<MainController>().Rotated = true;
                FindObjectOfType<GameSoundController>().RotatorEffect();
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Rotated!");
                Destroy(gameObject);
                break;
            case "shrinker":
                ShrinkBalls();
                Destroy(gameObject);
                break;
            case "slowy":
                SlowDown();
                Destroy(gameObject);
                break;
            case "biggrenade":
                Explode();
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, TNT!", Color.red);
                Destroy(gameObject);
                break;
            case "lightning":
                FindObjectOfType<PowerUpsController>().PickUpElectricity();
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Electricity added to inventory", Color.cyan);
                Destroy(gameObject);
                break;
            case "fire":
                FindObjectOfType<PowerUpsController>().PickUpFire();
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Fire added to inventory", Color.red);
                Destroy(gameObject);
                break;
            case "plasma":
                FindObjectOfType<PowerUpsController>().PickUpPlasma();
                FindObjectOfType<StatusController>().UpdateHistory("Random effect, Plasma added to inventory", Color.magenta);
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    private void GrowBalls()
    {
        var orbs = FindObjectOfType<OrbLauncher>().orbs;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];
            orb.transform.localScale += new Vector3(0.035f, 0.035f, 0);
        }
        FindObjectOfType<StatusController>().UpdateHistory("Random effect, Shot size doubled");
    }

    private void ShrinkBalls()
    {
        var launcher = FindObjectOfType<OrbLauncher>();
        var orbs = launcher.orbs;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];
            orb.transform.localScale -= new Vector3(0.035f, 0.035f, 0);

            if (orb.transform.localScale.x < 0.018f)
            {
                orb.transform.localScale = new Vector3(0.018f, 0.018f, 0);
            }
        }

        FindObjectOfType<GameSoundController>().ShrinkerEffect();
        FindObjectOfType<StatusController>().UpdateHistory("Random effect, Size decreased by half");
    }

    private void SlowDown()
    {
        var orbs = FindObjectOfType<OrbLauncher>().orbs;

        for (int i = 0; i < orbs.Count; i++)
        {
            var orb = orbs[i];

            if (orb != null)
            {
                orb.SlowDown ();
            }
        }

        FindObjectOfType<GameSoundController>().SlowyEffect();
        FindObjectOfType<StatusController>().UpdateHistory("Random effect, Speed decreased by half", textColours[1]);
    }

    public void Explode()
    {
        float distanceBetweenObjects = spawner.distanceBetweenObjects * Mathf.Sqrt(2) * 1.1f;
        distanceBetweenObjects *= distanceBetweenObjects;

        List<GameObject> ToDie = new List<GameObject>();

        var spawnedObjects = FindObjectOfType<SpawnHelper>().spawnedObjectsParent;

        for (int i = 0; i < spawnedObjects.childCount; i++)
        {
            GameObject G = spawnedObjects.GetChild(i).gameObject;

            if ((G.transform.position - transform.position).sqrMagnitude < distanceBetweenObjects)
            {
                ToDie.Add(G);
            }
        }

        foreach (GameObject g in ToDie)
        {
            if (g.GetComponent<Wall>())
            {
                break;
            }
            else if (g.GetComponent<Orb>())
            {
                break;
            }
            else if (g.GetComponent<BlackOut>())
            {
                g.GetComponent<BlackOut>().StopMe();
                Destroy(g);
                break;
            }
            else if (g.GetComponent<Pentagram>())
            {
                g.GetComponent<Pentagram>().hitsRemaining -= 100;
                break;
            }
            else if (g.GetComponent<Ooze>())
            {
                break;
            }
            else
            {
                FindObjectOfType<EffectController>().SmallBoom(g.transform.position);
                Destroy(g);
            }
        }

        FindObjectOfType<EffectController>().SmallBoom(gameObject.transform.position);
        FindObjectOfType<GameSoundController>().LineKillerEffect();
    }

    private void ChooseEffect()
    {
        int outcome = UnityEngine.Random.Range(3, 14);

        switch (outcome)
        {
            case 1:
                Effect = "metal";
                break;
            case 2:
                Effect = "ice";
                break;
            case 3:
                Effect = "downer";
                break;
            case 4:
                Effect = "abductor";
                break;
            case 5:
                Effect = "blackout";
                break;
            case 6:
                Effect = "grower";
                break;
            case 7:
                Effect = "rotator";
                break;
            case 8:
                Effect = "shrinker";
                break;
            case 9:
                Effect = "slowy";
                break;
            case 10:
                Effect = "biggrenade";
                break;
            case 11:
                Effect = "lightning";
                break;
            case 12:
                Effect = "fire";
                break;
            case 13:
                Effect = "plasma";
                break;
            default:
                break;
        }
    }
}