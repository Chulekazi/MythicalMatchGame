using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedMenuScript : MonoBehaviour
{
    public GameObject pausedScreenPanel;

    public void Play_Button()
    {
        pausedScreenPanel.SetActive(false);
    }

    public void Pause_Button()
    {
        pausedScreenPanel.SetActive(true);
    }

   /* public void Options_Button(string scene_Name)
    {
        SceneManager.LoadScene(scene_Name);
    }*/
}
