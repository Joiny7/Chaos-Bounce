using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {

    [SerializeField] Toggle invertedAimingToggle;
    [SerializeField] Toggle soundToggle;

    [SerializeField] Toggle cracksToggle;
    [SerializeField] Toggle numbersToggle;

    [SerializeField] Slider timeScaleSlider;
    [SerializeField] TextMeshProUGUI timeScaleValue;

    [SerializeField] Image soundButtonImage;

    [SerializeField] Sprite soundButtonImageOn;
    [SerializeField] Sprite soundButtonImageOff;

    public delegate void SettingsAction (bool state);
    public static event SettingsAction CracksSettingChanged;
    public static event SettingsAction NumbersSettingChanged;

    bool forwardAim;
    bool isSoundOn;

    bool cracksOn;
    bool numbersOn;

    float timeScale;

    void Start() {
        Load ();
        UpdateView ();
        SetListeners ();
    }

    void Load () {
        SettingsData settingsData = SaveSystem.LoadSettings ();

        forwardAim = settingsData.forwardAim;
        isSoundOn = settingsData.isSoundOn;
        timeScale = settingsData.timeScale;

        cracksOn = settingsData.cracksOn;
        numbersOn = settingsData.numbersOn;
    }

    void Save() {
        SaveSystem.SaveSettings (forwardAim, timeScale, isSoundOn, cracksOn, numbersOn);
    }

    void SetListeners() {
        invertedAimingToggle.onValueChanged.AddListener (ChangeAiming);
        soundToggle.onValueChanged.AddListener(ChangeSoundSettings);
        timeScaleSlider.onValueChanged.AddListener (ChangeTimeScaleSettings);

        cracksToggle.onValueChanged.AddListener (ChangeCracks);
        numbersToggle.onValueChanged.AddListener (ChangeNumbers);
    }

    void UpdateView() {
        invertedAimingToggle.isOn = !forwardAim;
        soundToggle.isOn = isSoundOn;

        cracksToggle.isOn = cracksOn;
        numbersToggle.isOn = numbersOn;

        timeScaleSlider.value = timeScale;
        timeScaleValue.text = timeScale.ToString ("0.##");
    }

    public void ChangeAiming(bool isInvertedAiming) {
        forwardAim = !isInvertedAiming;
        Save ();
    }

    public void ChangeSoundSettings (bool sound) {
        isSoundOn = sound;
        AudioListener.pause = !sound;
        Save ();
    }

    public void SwitchSoundSettings () {
        isSoundOn = !isSoundOn;
        AudioListener.pause = !isSoundOn;
        Save ();

        soundButtonImage.sprite = isSoundOn ? soundButtonImageOn : soundButtonImageOff;
    }

    public void ChangeTimeScaleSettings (float timeScale) {
        this.timeScale = timeScale;
        timeScaleValue.text = timeScale.ToString ("0.##");
        timeScaleSlider.value = timeScale;
        Save ();
    }

    public void ChangeCracks (bool cracks) {
        cracksOn = cracks;

        if (CracksSettingChanged != null) {
            CracksSettingChanged (cracksOn);
        }

        Save ();
    }

    public void ChangeNumbers (bool numbers) {
        numbersOn = numbers;

        if (NumbersSettingChanged != null) {
            NumbersSettingChanged (numbersOn);
        }

        Save ();
    }
}
