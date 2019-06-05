using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerUpsController : MonoBehaviour {

    public bool PlasmaOn;
    public bool FireOn;
    public bool TripleShotOn;
    public bool ElectricityOn;

    [SerializeField] TextMeshProUGUI MapKillerValueText;
    [SerializeField] TextMeshProUGUI PlasmaValueText;
    [SerializeField] TextMeshProUGUI FireValueText;
    [SerializeField] TextMeshProUGUI TripleShotValueText;
    [SerializeField] TextMeshProUGUI ElectricityValueText;

    [SerializeField] Animator animator;

    const string VALUE_PREFIX = "";

    private int mapKillerInventory;
    public int MapKillerInventory
    {
        get
        {
            return mapKillerInventory;
        }
        set
        {
            MapKillerValueText.text = VALUE_PREFIX + value;
            mapKillerInventory = value;
            animator.SetBool ("CanShow", isSomethingInInventory ());
        }
    }

    private int plasmaInventory;
    public int PlasmaInventory
    {
        get
        {
            return plasmaInventory;
        }
        set
        {
            PlasmaValueText.text = VALUE_PREFIX + value;
            plasmaInventory = value;
            animator.SetBool ("CanShow", isSomethingInInventory ());
        }
    }

    private int fireInventory;
    public int FireInventory
    {
        get
        {
            return fireInventory;
        }
        set
        {
            FireValueText.text = VALUE_PREFIX + value;
            fireInventory = value;
            animator.SetBool ("CanShow", isSomethingInInventory ());
        }
    }

    private int tripleShotInventory;
    public int TripleShotInventory
    {
        get
        {
            return tripleShotInventory;
        }
        set
        {
            TripleShotValueText.text = VALUE_PREFIX + value;
            tripleShotInventory = value;
            animator.SetBool ("CanShow", isSomethingInInventory ());
        }
    }

    private int electricityInventory;
    public int ElectricityInventory
    {
        get
        {
            return electricityInventory;
        }
        set
        {
            ElectricityValueText.text = VALUE_PREFIX + value;
            electricityInventory = value;
            animator.SetBool ("CanShow", isSomethingInInventory());
        }
    }

    private VisualOrb visualOrb;
    private LaunchPreview launchPreview;
    private GameSoundController gameSound;
    private EffectController effects;

    public delegate void PowerUpsControllerAction ();
    public static event PowerUpsControllerAction PowerUpUsed;
    public static event PowerUpsControllerAction FirePowerUpUsed;
    public static event PowerUpsControllerAction PlasmaPowerUpUsed;
    public static event PowerUpsControllerAction MapKillerPowerUpUsed;
    public static event PowerUpsControllerAction TripleShotPowerUpUsed;
    public static event PowerUpsControllerAction ElectricityShotPowerUpUsed;

    void Awake()
    {
        visualOrb = FindObjectOfType<VisualOrb> ();
        gameSound = FindObjectOfType<GameSoundController> ();
        launchPreview = FindObjectOfType<LaunchPreview> ();
        effects = FindObjectOfType<EffectController> ();
        LoadPowerUpValues ();
    }

    #region PickUp

    public void PickUpFire()
    {
        FireInventory++;
        animator.SetBool ("CanShow", true);
    }

    public void PickUpPlasma()
    {
        PlasmaInventory++;
        animator.SetBool ("CanShow", true);
    }

    public void PickUpTripleShot ()
    {
        TripleShotInventory++;
        animator.SetBool ("CanShow", true);
    }

    public void PickUpElectricity()
    {
        ElectricityInventory++;
        animator.SetBool ("CanShow", true);
    }

    public void PickUpMapKiller () {
        MapKillerInventory++;
        animator.SetBool ("CanShow", true);
    }

    #endregion

    #region Use

    public void TryToUseFire () {
        if(FireInventory <= 0) {
            return;
        }

        visualOrb.Fire (true);
        gameSound.FireEffect ();
        FireOn = true;
        FireInventory--;
        FindObjectOfType<StatusController>().UpdateHistory("Shots fired up!", Color.red);

        PowerUpUsed ();
        FirePowerUpUsed ();
    }

    public void TryToUsePlasma () {
        if (PlasmaInventory <= 0) {
            return;
        }

        visualOrb.Plasma (true);
        gameSound.PlasmaEffect ();
        PlasmaOn = true;
        PlasmaInventory--;
        FindObjectOfType<StatusController>().UpdateHistory("Shots plasmified!", Color.magenta);

        PowerUpUsed ();
        PlasmaPowerUpUsed ();
    }

    public void TryToUseTripleShot () {
        if (TripleShotInventory <= 0) {
            return;
        }

        launchPreview.ThreeShot = true;
        gameSound.TripleShotEffect ();
        TripleShotOn = true;
        TripleShotInventory--;
        FindObjectOfType<StatusController>().UpdateHistory("Triple shots this level!");

        PowerUpUsed ();
        TripleShotPowerUpUsed ();
    }

    public void TryToUseElectricity () {
        if (ElectricityInventory <= 0) {
            return;
        }

        visualOrb.Electricity (true);
        ElectricityOn = true;
        ElectricityInventory--;
        FindObjectOfType<StatusController>().UpdateHistory("Shots electrified!", Color.cyan);

        PowerUpUsed ();
        ElectricityShotPowerUpUsed ();
    }

    public void TryToUseMapKiller () {
        if (MapKillerInventory <= 0) {
            return;
        }

        StartCoroutine(MapBoom());

        PowerUpUsed ();
        MapKillerPowerUpUsed ();
    }

    #endregion

    private void LoadPowerUpValues() {
        PlayerData data = SaveSystem.LoadPlayer ();

        if (data == null) return;

        if (data.powerUps == null) return;

        MapKillerInventory = data.powerUps.mapKillerInventory;
        PlasmaInventory = data.powerUps.plasmaInventory;
        FireInventory = data.powerUps.fireInventory;
        TripleShotInventory = data.powerUps.tripleShotInventory;
        ElectricityInventory = data.powerUps.electricityInventory;
    }

    public PowerUps GetPowerUps() {
        return new PowerUps {
            mapKillerInventory = mapKillerInventory,
            plasmaInventory = plasmaInventory,
            fireInventory = fireInventory,
            tripleShotInventory = tripleShotInventory,
            electricityInventory = electricityInventory
        };
    }

    private bool isSomethingInInventory() {
        return MapKillerInventory + PlasmaInventory + FireInventory + TripleShotInventory + ElectricityInventory > 0;
    }

    private IEnumerator MapBoom()
    {
        FindObjectOfType<StatusController>().UpdateHistory("Nuke launched!", Color.green);
        effects.MapBoom();
        MapKillerInventory--;
        yield return new WaitForSeconds(0.25f);
        FindObjectOfType<SpawnHelper>().KillMap();
        yield return new WaitForSeconds (0.4f);
        FindObjectOfType<Spawner> ().SpawnRow ();
        yield break;
    }
}