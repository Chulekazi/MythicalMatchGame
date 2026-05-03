using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveGame
{
    public string scene_name;
    public int Heartpoints;

    public static class Save
    {
        public static string savePath = Application.persistentDataPath + "/save.json";
    }

    public static void Save_Game(string current_scene, int heartpoints)
    {
        SaveData data = new SaveData
        {
            scene = current_scene,
            playerHeartPoints = heartpoints
        };
    }
}
