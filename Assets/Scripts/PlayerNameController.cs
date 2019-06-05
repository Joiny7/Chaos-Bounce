using UnityEngine;
using TMPro;

public class PlayerNameController : MonoBehaviour {

    PlayerData data;
    public TMP_InputField input;

	void Start () {
        data = SaveSystem.LoadPlayer ();

        if (data == null) return;

        gameObject.SetActive (data.playerName == null);
    }

    public void SaveName2()
    {
        string name = input.text;
        SaveSystem.SavePlayer(data.highScore, data.currentScore, data.mapObjects, data.orbPosX, data.orbPosY, data.powerUps, data.orbAmount, name, data.userId);
    }

    public void SaveName(string name) {
        SaveSystem.SavePlayer (data.highScore, data.currentScore, data.mapObjects, data.orbPosX, data.orbPosY, data.powerUps, data.orbAmount, name, data.userId);
    }
}
