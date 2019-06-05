using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class AchievementSkin {
    public string achievementId;
    public UnityEvent onTurnedOn;
}

public class SkinChecker : MonoBehaviour {

    [SerializeField] List<AchievementSkin> toCheck;

	void Start () {
        foreach (AchievementSkin item in toCheck) {
            //Right now it will turn it on if player earned an achievement, could be changed 
            //to turn on by adding another playerPref with a prefix
            if (PlayerPrefs.GetInt (item.achievementId) == 1) {
                item.onTurnedOn.Invoke ();
            }
        }

        Destroy (this); // just remove component when it's done
    }
	
}
