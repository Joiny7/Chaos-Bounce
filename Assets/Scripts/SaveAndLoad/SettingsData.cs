using UnityEngine;

[System.Serializable]
public class SettingsData {

    public bool forwardAim;
    public float timeScale;

    public bool isSoundOn;

    public bool cracksOn = true;
    public bool numbersOn = true;


    public SettingsData (bool forward = true, float tScale = 1f, bool sound = true, bool cracks = true, bool numbers = true) {
        forwardAim = forward;
        timeScale = tScale;
        isSoundOn = sound;
        cracksOn = cracks;
        numbersOn = numbers;
    }
}

