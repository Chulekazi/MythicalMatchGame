using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public readonly DialogueManager manager1;
    public readonly separateManager manager2;
    public readonly onemoreManager manager3;

    public void LoadGameProgress()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            int savedIndex = PlayerPrefs.GetInt("SavedScene");
            SceneManager.LoadScene(savedIndex);
        }

    }

    public void NewGame()
    {
    
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

       
        PlayerData.PlayerHeartPoints = 0;
        PlayerData.playerName = "";
        PlayerData.clicked_ = new HashSet<string>();
        PlayerData.JournalEntries = new List<string>();

        manager1.DisplayPoints();
        manager2.DisplayPoints();
        manager3.DisplayPoints();
    }

}
