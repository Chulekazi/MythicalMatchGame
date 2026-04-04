using UnityEngine;
using UnityEngine.SceneManagement;

public class PronounManager : MonoBehaviour
{
    public static PronounManager instance;

    public Pronouns playerPronouns;

    void Awake()
    {
        if(instance == null)
        {
            //
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetPronouns(Pronouns pronouns)

    {
        //player's chosen pronouns
        playerPronouns = pronouns;
    }

   public string ReplacePronouns(string text)
    {
        var p = playerPronouns;
        return text.Replace("{SUBJECT}", p.subject)
            .Replace("{OBJECT}", p.obj);
            
    }
}
