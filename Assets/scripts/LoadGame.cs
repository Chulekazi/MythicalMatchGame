using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    public void LoadGameProgress()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            int savedIndex = PlayerPrefs.GetInt("SavedScene");
            SceneManager.LoadScene(savedIndex);
        }

    }

}
