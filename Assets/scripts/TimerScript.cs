using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public DialogueManager dialogue_Manager;
    public GameObject timesup;
    public GameObject continue_btn;
    public GameObject rewind_btn;
    public Image timerlinear;
    

    float timeremain;
    public float maxtime = 5.0f;
    public static bool rewindUsed = false;

    //private float time_remain;
    
    //"times up! pops up when the player has already pressed a choice button.
    //instead of freezing time we can make the rewind time slower

    void Start()
    {
        timeremain = maxtime;
        timesup.SetActive(false);

        StartCoroutine(RunTimer());
        
    }

    private IEnumerator RunTimer()
    {
        while (timeremain > 0)
        {
            timeremain -= Time.deltaTime;
            timerlinear.fillAmount = timeremain / maxtime;
            yield return null;
        }
            
        dialogue_Manager.choiceContainer.gameObject.SetActive(false);
                
    }


    public void TimesUpText()
    {
         timesup.SetActive(true);
    }
    

    public void RewindTime()
    {
        if(!rewindUsed)
        {
            rewindUsed = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //continue btn
        }
        else
        {
            rewind_btn.SetActive(false);
            continue_btn.SetActive(true);
            //audio sfx 
        }
       
    }

    
    public void NextDialogueScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
