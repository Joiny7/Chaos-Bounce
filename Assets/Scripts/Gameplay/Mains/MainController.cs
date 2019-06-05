using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public delegate void MainControllerActionParameter (int n);
    public delegate void MainControllerAction ();

    public static event MainControllerActionParameter CurrentScoreChanged;
    public static event MainControllerAction Rotator;

    public EffectController effects;

    private int currentScore;

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            if (CurrentScoreChanged != null) {
                CurrentScoreChanged (currentScore);
            }
        }
    }

    private Resolution thisDevice;
    Scene scene;
    public Camera cam;
    public SpriteRenderer visualOrbEdge;

    private PowerUpsController powerUpsController;

    public bool BlackOUT;
    public LayerMask BlackoutMask;
    public bool ForwardAim;
    public float myTimeScale = 1f;
    public bool Sound;

    private bool rotated;

    public bool Rotated
    {
        get
        {
            return rotated;
        }
        set
        {
            rotated = value;
            if (Rotator != null) {
                Rotator ();
            }
        }
    }

    public bool waitForEndRound;
    public int highScore;

    public GameObject LeftWall;
    public GameObject RightWall;
    public GameObject TopWall;
    public GameObject BottomWall;

    Animator cameraAnimator;

    [SerializeField]
    private bool endZoneOn;

    public bool getEndZoneOn
    {
        get { return endZoneOn; }
    }

    [SerializeField]
    private bool isSoundOn;

    public bool getIsSoundOn
    {
        get { return isSoundOn; }
    }

    [SerializeField]
    private bool isCracksOn;

    public bool getIsCracksOn
    {
        get { return isCracksOn; }
    }

    [SerializeField]
    private bool isNumbersOn;

    public bool getIsNumbersOn
    {
        get { return isNumbersOn; }
    }

    public OrbLauncher launcher;

    //PauseStuff
    public GameObject PauseButton;
    public GameObject PauseMenu;
    public bool paused;
    public PauseMenu pauseMenuScript;

    void Awake()
    {
        powerUpsController = FindObjectOfType<PowerUpsController> ();
        LoadPlayer ();
        LoadSettings ();
    }

    void Start()
    {
        if (myTimeScale < 1)
        {
            myTimeScale = 1;
            Time.timeScale = myTimeScale;
        }
    }

    void OnEnable () {
        SettingsController.CracksSettingChanged += CracksSettingChanged;
        SettingsController.NumbersSettingChanged += NumbersSettingChanged;
    }

    void OnDisable () {
        SettingsController.CracksSettingChanged -= CracksSettingChanged;
        SettingsController.NumbersSettingChanged -= NumbersSettingChanged;
    }

    public void Pause(bool showPauseMenu = true)
    {
        Time.timeScale = 0;
        paused = true;

        if (showPauseMenu) {
            ShowPauseMenu ();
        }
    }

    public void UnPause()
    {
        LoadSettings();
        HidePauseMenu();
        paused = false;
        SaveSystem.SaveSettings (ForwardAim, myTimeScale, isSoundOn, isCracksOn, isNumbersOn);
    }

    public void ResetGame() {
        SaveSystem.ResetPlayer ();
        Restart ();
    }

    public int UpdateHighScore()
    {
        PlayerData data = SaveSystem.LoadPlayer ();
        highScore = data != null ? data.highScore: 0;
        highScore = currentScore > highScore ? currentScore : highScore;
        return highScore;
    }

    void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer ();
        highScore = data.highScore;
        currentScore = data.currentScore;

        //copied and modified from orbReturn to place orb
        var pos = new Vector3 (data.orbPosX, data.orbPosY, visualOrbEdge.transform.position.z);

        visualOrbEdge.transform.position = pos;
        launcher.SetStartPos (pos);
        launcher.transform.position = pos;

        //launch data.orbAmount-1 orbs (1 more created in awake)
        for (int i = 1; i < data.orbAmount; i++) {
            launcher.CreateOrb ();
        }
    }

    void LoadSettings () {
        SettingsData settingsData = SaveSystem.LoadSettings ();
        ForwardAim = settingsData.forwardAim;
        SetIsSound (settingsData.isSoundOn);
        myTimeScale = settingsData.timeScale;
        Time.timeScale = settingsData.timeScale;

        isCracksOn = settingsData.cracksOn;
        isNumbersOn = settingsData.numbersOn;
    }

    private void CracksSettingChanged (bool isOn) {
        isCracksOn = isOn;
    }

    private void NumbersSettingChanged (bool isOn) {
        isNumbersOn = isOn;
    }

    private void ShowPauseMenu()
    {
        cam.cullingMask = pauseMenuScript.pauseOn;
        PauseButton.SetActive(false);
        PauseMenu.SetActive(true);     
    }

    private void HidePauseMenu()
    {
        pauseMenuScript.OpenGeneralPause();
        PauseMenu.SetActive(false);
        PauseButton.SetActive(true);

        if (BlackOUT)
        {
            cam.cullingMask = BlackoutMask;
        }
        else
        {
            cam.cullingMask = pauseMenuScript.pauseOff;
        }
    }

    public bool CheckForPause()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER

        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("PauseButton"))
                {
                    return true;                  
                }
            }
        }

#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began) {
                var hit = Physics2D.Raycast(Input.GetTouch(0).position, Vector2.zero);

                if (hit.collider != null) {
                    return hit.collider.tag == "PauseMenu";
                }
            }
        }

        return false;

#endif
        return false;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetIsSound (bool condition) {
        isSoundOn = condition;
        AudioListener.pause = !condition;
    }

    public void EndRound()
    {
        if (rotated)
        {
            if (waitForEndRound)
            {
                if(cameraAnimator == null) {
                    cameraAnimator = cam.GetComponent<Animator> ();
                }

                cameraAnimator.SetBool ("RotateBack", true);
                cameraAnimator.SetBool ("Rotate", false);

                rotated = false;
                waitForEndRound = false;
            }
            else
            {
                waitForEndRound = true;
            }
        }
    }
}