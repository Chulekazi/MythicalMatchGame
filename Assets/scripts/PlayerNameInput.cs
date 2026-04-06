using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNameInput : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void SavePlayerID()
    {
        PlayerData.playerName = nameInput.text;

        SceneManager.LoadScene("dialogue1");
    }
}
