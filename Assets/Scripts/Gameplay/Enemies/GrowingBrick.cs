using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrowingBrick : BaseObject {

    public int roundSpawned;
    private TextMeshPro text;
    private Spawner spawner;
    [SerializeField]
    private GrowingBrick growingBrickPrefab;
    private List<Vector2> openSpots = new List<Vector2>();
    private BlockAudioScript audioPlayer;
    private int startHits;
    public SpriteRenderer Damage;
    public Sprite[] cracks;
    public Sprite[] statesSprites;
    //public Color[] statesColours;
    public SpriteRenderer Body;
    private int reductionCounter = 0;

    private void Awake()
    {
        spawner = FindObjectOfType<Spawner>();
        text = GetComponentInChildren<TextMeshPro>();
        audioPlayer = GetComponentInChildren<BlockAudioScript>();
    }

    private void Start () {
        CracksSettingChanged (spawner.main.getIsCracksOn);
        NumbersSettingChanged (spawner.main.getIsNumbersOn);
    }

        private bool isQuitting = false;

    void OnApplicationQuit () {
        isQuitting = true;
    }

    private void OnDestroy()
    {
        if (!isQuitting) {
            Instantiate (Resources.Load ("DestroyEffect/" + System.Enum.GetName(typeof(DamageReductionState),state), typeof (GameObject)), transform.position, transform.rotation,transform.parent);
        }
        spawner.allEnemies.Remove(gameObject);
        spawner.growers.Remove(this);
        spawner.BlockDestroyed ();
    }

    public void FreezeBlock()
    {
        state = DamageReductionState.Ice;
        Body.sprite = statesSprites[0];
        //Damage.color = statesColours[0];
        UpdateCracks();
        UpdateVisualState();
    }

    public void MetalizeBlock()
    {
        state = DamageReductionState.Metal;
        Body.sprite = statesSprites[1];
        //Damage.color = statesColours[1];
        UpdateCracks();
        UpdateVisualState();
    }

    //huge same functionality for enemies
    void OnEnable () {
        SettingsController.CracksSettingChanged += CracksSettingChanged;
        SettingsController.NumbersSettingChanged += NumbersSettingChanged;
    }

    void OnDisable () {
        SettingsController.CracksSettingChanged -= CracksSettingChanged;
        SettingsController.NumbersSettingChanged -= NumbersSettingChanged;
    }

    private void UpdateCracks () {
        CracksSettingChanged (Damage.enabled);
    }

    private void UpdateVisualState () {
        NumbersSettingChanged (text.enabled);
    }

    private void CracksSettingChanged (bool isOn) {
        Damage.enabled = isOn;

        if (isOn) {
            float dmgProcent = (hitsRemaining / (float)startHits);
            int spriteIndex = 5 - Mathf.CeilToInt (dmgProcent / 0.2f);
            Damage.sprite = cracks[spriteIndex];
        }
    }

    private void NumbersSettingChanged (bool isOn) {
        text.enabled = isOn;

        if (isOn) {
            AdjustFontSize ();
            text.SetText (hitsRemaining.ToString ());
        }
    }

    //probably end of huge same functionality for enemies

    internal void SetHits(int hits)
    {
        hitsRemaining = hits;
        audioPlayer.StartPrep(hits);
        startHits = hits;
        UpdateVisualState();
    }

    private void AdjustFontSize()
    {
        if (hitsRemaining <= 99)
        {
            text.fontSize = 20f;
        }
        else if (100 <= hitsRemaining && hitsRemaining <= 999)
        {
            text.fontSize = 15f;
        }
        else if (hitsRemaining >= 1000 && hitsRemaining <= 9999)
        {
            text.fontSize = 13f;
        }
        else if (hitsRemaining >= 10000 && hitsRemaining <= 99999)
        {
            text.fontSize = 10f;
        }
        else
        {
            text.SetText("");
        }
    }

    public void Grow()
    {
        if (transform.position.y == (-3.1646))
        {
            return;
        }

        FindGrowingSpots();

        if (openSpots.Count < 1)
            return;

        var spot = Random.Range(0, openSpots.Count);
        var chosenSpot = openSpots[spot];
        var child = Instantiate(growingBrickPrefab, chosenSpot, Quaternion.identity, transform.parent);
        child.name = name;
        spawner.growers.Add(child);
        spawner.allEnemies.Add(child.gameObject);
        child.SetHits(hitsRemaining);
        child.roundSpawned = spawner.rowsSpawned;
    }

    private void FindGrowingSpots()
    {
        openSpots.Clear();

        CheckPosToGrow (Vector2.right);
        CheckPosToGrow (Vector2.left);
        CheckPosToGrow (Vector2.down);
    }

    private void CheckPosToGrow(Vector2 direction)
    {
        if (!Physics2D.Raycast (transform.position, direction, spawner.distanceBetweenObjects)) {
            openSpots.Add ((Vector2)transform.position + direction * spawner.distanceBetweenObjects);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.gameObject.GetComponent<Orb>().turnedToElectricity)
        {
            if (state != DamageReductionState.Ice)
            {
                GetComponent<ZappableObject>().InitialZap();
                hitsRemaining -= 4;
                if (hitsRemaining > 0)
                {
                    audioPlayer.PlayHit(hitsRemaining);
                    UpdateVisualState();
                    UpdateCracks();
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        if (col.collider.gameObject.GetComponent<Orb>().turnedToPlasma)
        {
            if (state == DamageReductionState.Normal)
            {
                FindObjectOfType<EffectController>().PlasmaDeath(gameObject.transform.position);
                Destroy(gameObject);
            }
            else if (state == DamageReductionState.Ice)
            {
                hitsRemaining += (-500);
            }
            else if (state == DamageReductionState.Metal)
            {
                hitsRemaining += (-200);
            }
            if (hitsRemaining > 0)
            {
                audioPlayer.PlayHit(hitsRemaining);
                UpdateVisualState();
                UpdateCracks();
            }
            else
            {
                FindObjectOfType<EffectController>().PlasmaDeath(gameObject.transform.position);
                Destroy(gameObject);
            }
        }
        if (col.collider.gameObject.GetComponent<Orb>().turnedToFire)
        {
            if (state == DamageReductionState.Normal)
            {
                FindObjectOfType<EffectController>().SmallSpark(gameObject.transform.position);
                hitsRemaining += (-2);
            }
            else if (state == DamageReductionState.Ice)
            {
                FindObjectOfType<EffectController>().SmallSpark(gameObject.transform.position);
                hitsRemaining += (-4);
            }
            else if (state == DamageReductionState.Metal)
            {
                FindObjectOfType<EffectController>().SmallSpark(gameObject.transform.position);
                hitsRemaining += (-3);
            }
            if (hitsRemaining > 0)
            {
                audioPlayer.PlayHit(hitsRemaining);
                UpdateVisualState();
                UpdateCracks();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (state == DamageReductionState.Normal)
            {
                hitsRemaining--;

                if (hitsRemaining > 0)
                {
                    audioPlayer.PlayHit(hitsRemaining);
                    UpdateVisualState();
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
                        audioPlayer.PlayHit(hitsRemaining);
                        UpdateVisualState();
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
                        audioPlayer.PlayHit(hitsRemaining);
                        UpdateVisualState();
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
    }

    public void TakeDamageFromZap()
    {
        hitsRemaining--;
        if (hitsRemaining > 0)
        {
            UpdateVisualState();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetState (DamageReductionState s) {
        state = s;
        switch (s) {
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
}