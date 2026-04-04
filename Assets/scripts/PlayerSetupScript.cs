using TMPro;
using UnityEngine;

public class PlayerSetupScript : MonoBehaviour
{
    public TMP_InputField player;

    public void ConfirmName()
    {
        string playerName = player.text;
        PlayerData.playerName = playerName;
        UnityEngine.SceneManagement.SceneManager.LoadScene("pronouns");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
