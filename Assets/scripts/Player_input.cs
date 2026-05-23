using TMPro;
using UnityEngine;

public class Player_input : MonoBehaviour
{
    public TMP_InputField name_player;
    public BIGMANAGER manager;
    public Dialogue_line first_line;
    public void Confirm_name()
    {
        if (!string.IsNullOrEmpty(name_player.text))
        {
            Player_profile.Instance.SetPlayerName(name_player.text);
            gameObject.SetActive(false);
            manager.Start_Dialogue(first_line);
        }
    }
}
