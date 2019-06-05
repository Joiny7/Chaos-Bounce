using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour {

    public AchievementDatabase db;

    public AchievementNotificationController achievementNotificationController;

    public DropdownController dropdownController;

    public GameObject achievementItemPrefab;
    public Transform content;

    public Achievements achievementsToShow;

    [SerializeField][HideInInspector]
    private List<AchievementItemController> achievementItems;

    private void Start () {
        dropdownController.onValueChanged += HandleAchievementDropdownValueChanged;
        LoadAchievementsTable ();
    }

    private void HandleAchievementDropdownValueChanged (Achievements achievement) {
        achievementsToShow = achievement;
    }

    public void ShowNotification() {
        Achievement achievement = db.achievements[(int)achievementsToShow];
        achievementNotificationController.ShowNotification (achievement);
    }

    [ContextMenu("LoadAchievementsTable()")]
    public void LoadAchievementsTable() {
        foreach (AchievementItemController achievementItem in achievementItems) {
            DestroyImmediate (achievementItem.gameObject);
        }
        achievementItems.Clear ();
        foreach (Achievement achievement in db.achievements) {
            GameObject obj = Instantiate (achievementItemPrefab, content);
            AchievementItemController controller = obj.GetComponent<AchievementItemController> ();
            bool unlocked = PlayerPrefs.GetInt (achievement.id, 0) == 1;

            controller.unlocked = unlocked;
            controller.achievement = achievement;
            controller.RefreshView ();

            achievementItems.Add (controller);
        }
    }

    public void UnlockAchievement(Achievements achievement) {
        AchievementItemController item = achievementItems[(int)achievement];

        if(item.unlocked) {
            return;
        }

        ShowNotification ();
        PlayerPrefs.SetInt (item.achievement.id, 1);
        item.unlocked = true;
        item.RefreshView ();
    }

    public void UnlockAchievement() {
        UnlockAchievement (achievementsToShow);
    }

    public void LockAllAchievements() {
        foreach (Achievement achievement in db.achievements) {
            PlayerPrefs.DeleteKey (achievement.id);
        }

        foreach (AchievementItemController achievementItem in achievementItems) {
            achievementItem.unlocked = false;
            achievementItem.RefreshView ();
        }
    }
}
