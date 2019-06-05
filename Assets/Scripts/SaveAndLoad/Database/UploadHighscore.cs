using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UploadHighscore : MonoBehaviour {

    private Guid _apiKey = new Guid("87A8A96F-8A80-4DB2-97B6-CD13DE27C368");
    private string Url = "https://chaosbouncescores.azurewebsites.net/api/highscore/";
    private string Url2 = "https://localhost:44349/api/highscore/";

    public int existingScore = 0;

    public IEnumerator PostHighScore(string name, int score)
    {
        DatabaseModels.FirstTimeRootObject obj = new DatabaseModels.FirstTimeRootObject(name, score);
        var request = new UnityWebRequest(Url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(obj));
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apiKey", _apiKey.ToString());
        request.SetRequestHeader("Content-Type", "application/json");
        //yield return request.SendWebRequest();
        request.SendWebRequest();

        yield return new WaitUntil(() => request.isDone);

        if (request.responseCode != (long)200)
        {
            SaveSystem.SetIsHighScoreInDB(false);
            //set setting to "not uploaded"
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.text);
            Debug.Log(request.responseCode);
            yield break;
        }
        else
        {
            //setting to uploaded
            SaveSystem.SetIsHighScoreInDB(true);
            //take down userID & save
            DatabaseModels.RootObject2 root2 = JsonUtility.FromJson<DatabaseModels.RootObject2>(request.downloadHandler.text);
            DatabaseModels.RootObject root = JsonUtility.FromJson<DatabaseModels.RootObject>(request.downloadHandler.text);
            root.userId = new Guid(root2.userId);
            SaveSystem.SaveUserID(root.userId);
            //SaveSystem.SaveUserID(JsonUtility.FromJson<DatabaseModels.RootObject>(request.downloadHandler.text).userId);
            yield break;
        }
    }

    //Test me
    public IEnumerator UpdateHighscore(Guid? userId, string name, int score)
    {
        //DatabaseModels.RootObject obj = new DatabaseModels.RootObject(userId, name, score);
        DatabaseModels.RootObject2 obj = new DatabaseModels.RootObject2(userId.GetValueOrDefault().ToString(), name, score);
        var request = new UnityWebRequest(Url, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(JsonUtility.ToJson(obj));
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("apiKey", _apiKey.ToString());
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();

        yield return new WaitUntil(() => request.isDone);

        if (request.responseCode == 200)
        {
            //setting to uploaded
            SaveSystem.SetIsHighScoreInDB (true);
            yield break;
        }
        else
        {
            //set setting to "not uploaded"
            SaveSystem.SetIsHighScoreInDB (false);
            Debug.Log(request.error);
            Debug.Log(request.downloadHandler.text);
            Debug.Log(request.responseCode);
            yield break;
        }

    }

    //change me to accept userId
    public IEnumerator GetMyScore()
    {
        PlayerData playerData = SaveSystem.LoadPlayer ();
        string id = playerData.userId.GetValueOrDefault().ToString();

        UnityWebRequest request = UnityWebRequest.Get (Url + id);
        request.SetRequestHeader("apiKey", _apiKey.ToString());
        request.SendWebRequest();

        yield return new WaitUntil(() => request.isDone);

        DatabaseModels.RootObject root = JsonUtility.FromJson<DatabaseModels.RootObject>(request.downloadHandler.text);
        DatabaseModels.RootObject2 root2 = JsonUtility.FromJson<DatabaseModels.RootObject2>(request.downloadHandler.text);

        //DatabaseModels.RootObject root = JsonUtility.FromJson<DatabaseModels.RootObject>(request.downloadHandler.text);

        if (root != null)
        {
            existingScore = root.score;
        }
        yield break;
    }
}