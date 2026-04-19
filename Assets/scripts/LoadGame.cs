using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
   
    /*public void Load_Game()
    {
        string path = Application.persistentDataPath + "save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            SceneManager.LoadScene(data.scene);
            game_manager.line_index = data.dialogueProgress;
            PlayerData.playerName = data.playerName;
        }
    }*/

}
