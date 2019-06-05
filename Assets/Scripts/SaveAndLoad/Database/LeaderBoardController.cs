using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;

public class LeaderBoardController : MonoBehaviour {

    private Guid _apiKey = new Guid("87A8A96F-8A80-4DB2-97B6-CD13DE27C368");
    private string Url = "https://chaosbouncescores.azurewebsites.net/api/highscore/";

    public TextMeshProUGUI[] Names;
    public TextMeshProUGUI[] Scores;

    private void OnEnable()
    {
        StartCoroutine(GetTop10());
    }

    public IEnumerator GetTop10()
    {
        UnityWebRequest request = UnityWebRequest.Get(Url);
        yield return request.SendWebRequest();
        UpdateBoard(request.downloadHandler.text);
    }

    private void UpdateBoard(string jsonString)
    {
        DatabaseModels.BoardList list3 = JsonUtility.FromJson<DatabaseModels.BoardList>("{\"rootObjects\":" + jsonString + "}");

        for (int i = 0; i < 10; i++)
        {
            var obj = list3.rootObjects[i];
            Names[i].text = obj.name;
            Scores[i].text = obj.score.ToString();
        }
    }
}