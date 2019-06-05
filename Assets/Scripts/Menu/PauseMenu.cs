using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public MainController main;
    public GameObject settingsPanel;
    public GameObject settings;
    public GameObject generalButtons;
    public GameObject howTo;

    public Text title;
    public LayerMask pauseOn;
    public LayerMask pauseOff;

    public void OpenSettings()
    {
        if (main.paused)
        {
            generalButtons.SetActive(false);
            title.text = "Settings";
            settings.SetActive(true);
            settingsPanel.SetActive (true);
            howTo.SetActive(false);
        }      
    }

    public void OpenGeneralPause()
    {
        if (main.paused)
        {
            settings.SetActive(false);
            settingsPanel.SetActive (false);
            title.text = "Paused";
            generalButtons.SetActive(true);
            howTo.SetActive(false);
        }
    }

    public void OpenHowTwo()
    {
        if (main.paused)
        {
            howTo.SetActive(true);
            title.text = "How To play";
            generalButtons.SetActive(false);
            settings.SetActive(false);
            settingsPanel.SetActive (false);
        }
    }
}
