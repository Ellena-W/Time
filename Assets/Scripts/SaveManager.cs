using UnityEngine;
using System.IO;
public class SaveManager : MonoBehaviour
{
    public SaveData data;

    public static SaveManager instance;

    public string SaveFilePath;

    private  void Start()
    {
        if (instance != null & instance != this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        SaveFilePath = $"Application.persistentDataPath)/saveFilePath/save.json";

        LoadFromFile();
        Debug.Log(Application.persistentDataPath);
    }
    public void LoadFromFile()
    {
        if (!File.Exists(SaveFilePath))
        {
            return;
        }
        string json = File.ReadAllText(SaveFilePath);
        JsonUtility.FromJsonOverwrite(json, data);
    }
    public void SavetoFile()
    {
        if (!File.Exists(SaveFilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(SaveFilePath));
            File.CreateText(SaveFilePath);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(SaveFilePath, json);
    }
}
