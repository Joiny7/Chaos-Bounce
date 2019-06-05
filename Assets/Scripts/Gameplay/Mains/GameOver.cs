using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour {

    public MainController main;
    public TextMeshProUGUI Snark;
    public TextMeshProUGUI scoreValue;
    public TextMeshProUGUI highScoreValue;
    public string[] Snarks;

    private void OnEnable()
    {
        SetGameOverScoreText();
    }

    private void SetGameOverScoreText()
    {
        scoreValue.text = main.CurrentScore.ToString();
        highScoreValue.text = main.highScore.ToString();
        Snark.text = Snarks[Random.Range(0, Snarks.Length + 1)];
    }
}