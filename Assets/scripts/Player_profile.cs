using UnityEngine;

public class Player_profile : MonoBehaviour
{
    public static Player_profile Instance;
    public string player_name = "";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetPlayerName(string newName)
    {
        player_name = newName;
    }
}
