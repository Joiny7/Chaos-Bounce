using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTester : MonoBehaviour {

    public AchievementManager achievementManager;

	void Update () {

        achievementManager.UnlockAchievement (Achievements.EARTHDIED1);

	}
}
