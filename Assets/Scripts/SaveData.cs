using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Vector3 position;
    public string playerData;
    public int currentLevel;
}

[System.Serializable]
public class PlayerData
{
    public string name;
    public int favoriteNumber;
}