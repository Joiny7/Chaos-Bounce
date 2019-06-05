using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    //Main menu objects
    public GameObject MainMenu;
    public GameObject SettingsMenu;
    public GameObject HowToMenu;
    public GameObject CreditsMenu;
    public GameObject LeaderboardMenu;
    public GameObject Moon;

    //How To Pages
    public GameObject PowerupsMatrix;
    public GameObject BlocksMatrix;
    public GameObject DebrisMatrix;
    public GameObject RandomizersMatrix;

    void Start()
    {
        AudioListener.pause = !SaveSystem.LoadSettings ().isSoundOn;
    }

    public void PlayNewGame() {
        SaveSystem.ResetPlayer ();
        Time.timeScale = SaveSystem.LoadSettings ().timeScale;
        SceneManager.LoadScene("Game");
    }

    public void ResumeGame () {
        Time.timeScale = SaveSystem.LoadSettings().timeScale;
        SceneManager.LoadScene ("Game");
    }

    public void OpenSettings()
    {
        AllOff();
        SettingsMenu.SetActive(true);
    }

    public void OpenLeaderboard()
    {
        AllOff();
        LeaderboardMenu.SetActive(true);
    }

    public void OpenMainMenu()
    {
        AllOff();
        Moon.SetActive(true);
        MainMenu.SetActive(true);
    }

    //How To
    public void OpenHowToMenu()
    {
        AllOff();
        HowToMenu.SetActive(true);
        BlocksMatrix.SetActive(true);
    }

    public void BlocksToPowerUps()
    {
        AllOff();
        HowToMenu.SetActive(true);
        PowerupsMatrix.SetActive(true);
    }

    public void PowerToRandom()
    {
        AllOff();
        HowToMenu.SetActive(true);
        RandomizersMatrix.SetActive(true);
    }

    public void RandomToDebris()
    {
        AllOff();
        HowToMenu.SetActive(true);
        DebrisMatrix.SetActive(true);
    }

    public void OpenCreditsMenu()
    {
        AllOff();
        CreditsMenu.SetActive(true);
        BlocksMatrix.SetActive(true);
    }

    private void AllOff()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(false);
        HowToMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        BlocksMatrix.SetActive(false);
        PowerupsMatrix.SetActive(false);
        DebrisMatrix.SetActive(false);
        RandomizersMatrix.SetActive(false);
        LeaderboardMenu.SetActive(false);
        Moon.SetActive(false);
    }
}