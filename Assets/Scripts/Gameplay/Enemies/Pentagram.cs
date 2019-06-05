using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pentagram : BaseObject {

    private EffectController effects;
    private OrbLauncher launcher;
    private Spawner spawner;
    public List<DebrisOrb> dOrbsBouncy = new List<DebrisOrb>();
    private GameSoundController gameSound;
    [SerializeField]
    private DebrisOrb dOrbBouncy;
    private int startHits;
    private PowerUpsController powerUpsController;
    public SpriteRenderer Damage;
    public Sprite[] cracks;
    public Sprite[] statesSprites;
    //public Color[] statesColours;
    public SpriteRenderer Body;
    private int reductionCounter = 0;

    private void Awake()
    {
        powerUpsController = FindObjectOfType<PowerUpsController> ();
        effects = FindObjectOfType<EffectController>();
        launcher = FindObjectOfType<OrbLauncher>();
        spawner = FindObjectOfType<Spawner>();
        gameSound = FindObjectOfType<GameSoundController>();
        PopulateList();
        hitsRemaining = 666;
        startHits = hitsRemaining;
    }

    private void Start () {
        CracksSettingChanged (spawner.main.getIsCracksOn);
    }

    private bool isQuitting = false;

    void OnApplicationQuit () {
        isQuitting = true;
    }

    private void OnDestroy () {
        if (!isQuitting) {
            Instantiate (Resources.Load ("DestroyEffect/" + System.Enum.GetName (typeof (DamageReductionState), state), typeof (GameObject)), transform.position, transform.rotation);
        }
        spawner.allEnemies.Remove(gameObject);
        spawner.BlockDestroyed ();
    }

    public void FreezeBlock()
    {
        state = DamageReductionState.Ice;
        Body.sprite = statesSprites[0];
        //Damage.color = statesColours[0];
        UpdateCracks();
    }

    public void MetalizeBlock()
    {
        state = DamageReductionState.Metal;
        Body.sprite = statesSprites[1];
        //Damage.color = statesColours[1];
        UpdateCracks();
    }

    public void SetState(DamageReductionState s) {
        state = s;
        switch(s) {
            case DamageReductionState.Ice:
                Body.sprite = statesSprites[0];
                break;
            case DamageReductionState.Metal:
                Body.sprite = statesSprites[1];
                break;
            default:
                break;
        }
        UpdateCracks ();
    }

    //huge same functionality for enemies

    void OnEnable () {
        SettingsController.CracksSettingChanged += CracksSettingChanged;
    }

    void OnDisable () {
        SettingsController.CracksSettingChanged -= CracksSettingChanged;
    }

    private void UpdateCracks () {
        CracksSettingChanged (Damage.enabled);
    }

    private void CracksSettingChanged (bool isOn) {
        Damage.enabled = isOn;

        if (isOn) {
            float dmgProcent = (hitsRemaining / (float)startHits);
            int spriteIndex = 5 - Mathf.CeilToInt (dmgProcent / 0.2f);
            Damage.sprite = cracks[spriteIndex];
        }
    }

    //probably end of huge same functionality for enemies
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            var orb = col.gameObject.GetComponent<Orb>();
            gameSound.PlayHitPentagram();
            effects.DarkSpark(gameObject.transform.position);

            if (col.collider.gameObject.GetComponent<Orb>().turnedToElectricity)
            {
                if (state != DamageReductionState.Ice)
                {
                    GetComponent<ZappableObject>().InitialZap();
                    hitsRemaining -= 2;
                }
            }
            if (orb.turnedToPlasma || orb.turnedToFire)
            {
                if (orb.turnedToPlasma)
                {
                    if (state == DamageReductionState.Normal)
                    {
                        hitsRemaining -= 30;
                    }
                    else if (state == DamageReductionState.Ice)
                    {
                        hitsRemaining -= 20;
                    }
                    else if (state == DamageReductionState.Metal)
                    {
                        hitsRemaining -= 10;
                    }
                }
                else
                {
                    if (state == DamageReductionState.Normal)
                    {
                        hitsRemaining -= 2;
                    }
                    else if (state == DamageReductionState.Ice)
                    {
                        hitsRemaining -= 4;
                    }
                    else if (state == DamageReductionState.Metal)
                    {
                        hitsRemaining -= 3;
                    }
                }
            }
            else
            {
                if (state == DamageReductionState.Normal)
                {
                    hitsRemaining--;

                    if (hitsRemaining > 0)
                    {
                        UpdateCracks();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
                else if (state == DamageReductionState.Ice)
                {
                    reductionCounter++;

                    if (reductionCounter == 2)
                    {
                        hitsRemaining--;

                        if (hitsRemaining > 0)
                        {
                            UpdateCracks();
                        }
                        else
                        {
                            Destroy(gameObject);
                        }

                        reductionCounter = 0;
                    }
                }
                else if (state == DamageReductionState.Metal)
                {
                    reductionCounter++;

                    if (reductionCounter == 5)
                    {
                        hitsRemaining--;

                        if (hitsRemaining > 0)
                        {
                            UpdateCracks();
                        }
                        else
                        {
                            Destroy(gameObject);
                        }

                        reductionCounter = 0;
                    }
                }
            }
            if (hitsRemaining < 1)
            {
                gameSound.PlayDestroyPentagram();
                PowerOfDarkness();
                Destroy(gameObject);
            }
        }

        //UpdateCracks();
    }

    private void PopulateList()
    {
        for (int i = 0; i < 50; i++)
        {
            var orb = Instantiate(dOrbBouncy, gameObject.transform.position, Quaternion.identity);
            orb.gameObject.SetActive(false);
            dOrbsBouncy.Add(orb);
        }
    }

    public void PowerOfDarkness()
    {
        for (int i = 0; i < dOrbsBouncy.Count; i++)
        {            
            var orb = dOrbsBouncy[i];
            orb.gameObject.SetActive(true);
            orb.transform.position = gameObject.transform.position;
            Vector2 random = new Vector2(Random.Range(10f, 100f), Random.Range(10f, 100f));
            orb.GetComponent<Rigidbody2D>().AddForce(random);
            launcher.CreateOrb();
        }

        effects.DarkSparkBig(gameObject.transform.position);
        spawner.allEnemies.Remove(gameObject);
        powerUpsController.PickUpFire();
        powerUpsController.TryToUseFire();
        FindObjectOfType<StatusController>().UpdateHistory("Demonic power acquired", Color.red);
    }
}