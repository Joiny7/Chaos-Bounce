using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem {
    static string playerPath = Application.persistentDataPath + "/player.player";
    static string settingsPath = Application.persistentDataPath + "/settings.settings";

    public static void SavePlayer (int highScore, int currentScore, MapObject[] mapObjects, float orbPosX, float orbPosY, PowerUps powerUps = default(PowerUps), int orbAmount = 1, string name = null, Guid? id = null) {
        PlayerData playerData = new PlayerData (highScore, currentScore, mapObjects, orbPosX, orbPosY, powerUps, orbAmount, name, id);

        SaveToFile (playerPath, playerData);
    }

    public static PlayerData LoadPlayer () {
        PlayerData data = LoadFromFile<PlayerData> (playerPath);
        return data ?? new PlayerData ();
    }


    public static void SaveSettings (bool forwardAim, float timeScale, bool sound, bool cracks = true, bool numbers = true) {
        SettingsData settingsData = new SettingsData (forwardAim, timeScale, sound, cracks, numbers);

        SaveToFile (settingsPath, settingsData);
    }

    public static SettingsData LoadSettings () {
        SettingsData data = LoadFromFile<SettingsData> (settingsPath);
        return data ?? new SettingsData ();
    }

    public static void ResetPlayer() {
        if (File.Exists (playerPath))
        {
            PlayerData data = LoadPlayer();
            SavePlayer(data.highScore, 0, null, 0f, -4f, null, 1, data.playerName, data.userId);
        } else {
            //Debug.Log("Save File not found in " + playerPath);
        }
    }

    public static void SetIsHighScoreInDB (bool isHighScoreInDB) {
        PlayerData data = LoadPlayer ();
        SavePlayer (data.highScore, data.currentScore, data.mapObjects, data.orbPosX, data.orbPosY, data.powerUps, data.orbAmount, data.playerName, data.userId);
    }

    public static void SaveUserID (Guid? id) {
        PlayerData data = LoadPlayer ();
        SavePlayer (data.highScore, data.currentScore, data.mapObjects, data.orbPosX, data.orbPosY, data.powerUps, data.orbAmount, data.playerName, id);
    }

    public static bool GameCanBeResumed() {
        return File.Exists (playerPath);
    }

    static void SaveToFile (string path, object obj) {
        BinaryFormatter formatter = new BinaryFormatter ();
        FileStream stream = new FileStream (path, FileMode.Create);

        formatter.Serialize (stream, obj);
        stream.Close ();
    }

    static T LoadFromFile<T>(string path) {
        if (File.Exists (path)) {
            BinaryFormatter formatter = new BinaryFormatter ();
            FileStream stream = new FileStream (path, FileMode.Open);

            T data = (T)formatter.Deserialize (stream);
            stream.Close ();

            return data;
        } else {
            //Debug.Log("Save File not found in " + path);
            return default(T);
        }
    }
}
