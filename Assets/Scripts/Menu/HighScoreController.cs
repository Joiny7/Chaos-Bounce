using TMPro;
using UnityEngine;

public class HighScoreController : MonoBehaviour {

    public TextMeshProUGUI current;
    public TextMeshProUGUI high;

    void Start () {
        PlayerData data = SaveSystem.LoadPlayer ();
        string hs = data != null ? data.highScore.ToString() : "0";
        string cs = data != null ? data.currentScore.ToString() : "0";
        high.text = hs;
        current.text = cs;
    }
}