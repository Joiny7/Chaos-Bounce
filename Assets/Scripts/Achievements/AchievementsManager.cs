using System.Collections.Generic;
using UnityEngine;

public class AchievementsManager : MonoBehaviour {

    [SerializeField] AchievementDatabase achievementDatabase;

    public bool CleanAchievements = false;

    public AchievementNotificationController achievementNotificationController;

    private void Awake () {
        if (CleanAchievements) {
            DeletePlayerPrefs ();
        }
    }

    void DeletePlayerPrefs() {
        PlayerPrefs.DeleteAll ();
        Debug.Log ("Player Prefs Cleaned! All achievement progress reset complete.");
    }

    void OnEnable () {
        MainController.CurrentScoreChanged += ScoreChanged;
        OrbLauncher.OrbNumberChanged += OrbNumberChanged;
        MainController.Rotator += Rotator;
        LoserLine.GameOver += GameOver;

        PowerUpsController.PowerUpUsed += PowerUpUsed;
        PowerUpsController.FirePowerUpUsed += FirePowerUpUsed;
        PowerUpsController.PlasmaPowerUpUsed += PlasmaPowerUpUsed;
        PowerUpsController.MapKillerPowerUpUsed += MapKillerPowerUpUsed;
        PowerUpsController.TripleShotPowerUpUsed += TripleShotPowerUpUsed;
        PowerUpsController.ElectricityShotPowerUpUsed += ElectricityPowerUpUsed;

        Spawner.OneMoreBlockDestroyed += OneMoreDestroyed;
        Spawner.IcePickedUp += IcePickedUp;
        Spawner.MetalPickedUp += MetalPickedUp;
        Spawner.FlippyFloopPickedUp += FlippyFloopPickedUp;
    }

    void OnDisable () {
        MainController.CurrentScoreChanged -= ScoreChanged;
        OrbLauncher.OrbNumberChanged -= OrbNumberChanged;
        MainController.Rotator -= Rotator;
        LoserLine.GameOver -= GameOver;

        PowerUpsController.PowerUpUsed -= PowerUpUsed;
        PowerUpsController.FirePowerUpUsed -= FirePowerUpUsed;
        PowerUpsController.PlasmaPowerUpUsed -= PlasmaPowerUpUsed;
        PowerUpsController.MapKillerPowerUpUsed -= MapKillerPowerUpUsed;
        PowerUpsController.TripleShotPowerUpUsed -= TripleShotPowerUpUsed;
        PowerUpsController.ElectricityShotPowerUpUsed -= ElectricityPowerUpUsed;

        Spawner.OneMoreBlockDestroyed -= OneMoreDestroyed;
        Spawner.IcePickedUp -= IcePickedUp;
        Spawner.MetalPickedUp -= MetalPickedUp;
        Spawner.FlippyFloopPickedUp -= FlippyFloopPickedUp;
    }

    void ScoreChanged (int score) {
        CheckIncrementalAchievementType (AchievementPropertyType.Levels, score);
    }

    void OrbNumberChanged () {
        CheckIncrementalAchievementType (AchievementPropertyType.Orbs);
    }

    void Rotator () {
        CheckIncrementalAchievementType (AchievementPropertyType.Rotator);
    }

    void GameOver () {
        CheckIncrementalAchievementType (AchievementPropertyType.GameOvers);
    }

    void PowerUpUsed () {
        CheckIncrementalAchievementType (AchievementPropertyType.PowerUps);
    }

    void FirePowerUpUsed () {
        CheckIncrementalAchievementType (AchievementPropertyType.Fire);
    }

    void PlasmaPowerUpUsed () {
        CheckIncrementalAchievementType (AchievementPropertyType.Plasma);
    }

    void MapKillerPowerUpUsed () {
        CheckIncrementalAchievementType (AchievementPropertyType.MapKiller);
    }

    void TripleShotPowerUpUsed () {
        CheckIncrementalAchievementType (AchievementPropertyType.TripleShot);
    }

    void ElectricityPowerUpUsed () {
        CheckIncrementalAchievementType (AchievementPropertyType.Electricity);
    }

    void OneMoreDestroyed () {
        CheckIncrementalAchievementType (AchievementPropertyType.Enemies);
    }

    void IcePickedUp () {
        CheckIncrementalAchievementType (AchievementPropertyType.Ice);
    }

    void MetalPickedUp () {
        CheckIncrementalAchievementType (AchievementPropertyType.Metal);
    }

    void FlippyFloopPickedUp () {
        CheckIncrementalAchievementType (AchievementPropertyType.Flippyfloop);
    }

    void CheckIncrementalAchievementType (AchievementPropertyType type, int amount = 1) {
        foreach (Achievement achievement in achievementDatabase.achievements) {
            if (achievement.property.type == type) {
                if(achievement.property.incrementsAcrossSessions) {
                    amount = PlayerPrefs.GetInt (achievement.id + "SAVED", 0) + 1;
                }

                if (amount >= achievement.property.amount && PlayerPrefs.GetInt (achievement.id, 0) == 0) {
                    UnlockAhievement (achievement);
                } else {
                    PlayerPrefs.SetInt (achievement.id + "SAVED", amount);
                }
            }
        }
    }

    void UnlockAhievement(Achievement achievement) {
        achievementNotificationController.ShowNotification (achievement);
        PlayerPrefs.SetInt (achievement.id, 1);
    }
}
