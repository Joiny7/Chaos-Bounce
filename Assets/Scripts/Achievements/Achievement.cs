[System.Serializable]
public class Achievement {
    public string id;
    public string title;
    public string description;
    public AchievementProperty property;
}

[System.Serializable]
public class AchievementProperty {
    public AchievementPropertyType type;
    public int amount;
    public bool incrementsAcrossSessions = false;
}

[System.Serializable]
public enum AchievementPropertyType {
    OneTime,
    Levels,
    PowerUps,
    GameOvers,
    Orbs,
    Ice,
    Metal,
    Rotator,
    Flippyfloop,
    Enemies,
    Fire,
    Plasma,
    MapKiller,
    TripleShot,
    Electricity,
}