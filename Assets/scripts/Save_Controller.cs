using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Save_Controller
{
    public static string savePath = Application.persistentDataPath + "/save.json";
    public static void SaveData(string currentScene, int heartpoints)
    {
        SaveData data = new SaveData
        {
            scene = currentScene,
            playerHeartPoints = heartpoints
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
        Debug.Log("Game saved: " + savePath);
    }
}
