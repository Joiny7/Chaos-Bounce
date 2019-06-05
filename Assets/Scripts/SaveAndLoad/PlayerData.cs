using System;

[System.Serializable]
public class PlayerData {

    public string playerName;
    public Guid? userId;

    public int highScore;
    public int currentScore;

    public bool isHighScoreInDB;

    public MapObject[] mapObjects;

    public float orbPosX;
    public float orbPosY;
    public int orbAmount;

    public PowerUps powerUps;

    public PlayerData (int hs = 0, int cs = 0, MapObject[] mapObjects = null, float orbPosX = 0, float orbPosY = -4f, PowerUps powerUps = null, int orbAmount = 1, string name = null, Guid? id = null) {
        highScore = hs;
        currentScore = cs;

        this.mapObjects = mapObjects;

        this.orbPosX = orbPosX;
        this.orbPosY = orbPosY;
        this.orbAmount = orbAmount;

        this.powerUps = powerUps;

        this.playerName = name;

        this.userId = id;
    }
}

[System.Serializable]
public class MapObject {
    public int xIndex;
    public float y;

    public int hitsLeft;

    public int damageReductionStateInt;

    public string name;

    public MapObject (int xIndex, float posY, int hitsLeft, string name, int damageReductionStateInt) {
        this.xIndex = xIndex;

        this.y = posY;

        this.hitsLeft = hitsLeft;

        this.name = name;

        this.damageReductionStateInt = damageReductionStateInt;
    }
}

[System.Serializable]
public class PowerUps {
    public int mapKillerInventory;
    public int plasmaInventory;
    public int fireInventory;
    public int tripleShotInventory;
    public int electricityInventory;
}


