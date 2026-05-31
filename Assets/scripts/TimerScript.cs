using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public DialogueManager dialogue_Manager;

    public GameObject continue_btn; //button to display next dialogue line
    public AudioClip rewindSFX;
    public AudioSource audioSource;

    public Button rewind_btn; //rewind time button

    public Image timerlinear; //slider

    public TMP_Text dialogue_text; //dialogue text, mainly for the quiz questions
    
    float timeremain;
    public float maxtime = 5.0f;
    public static bool rewindUsed = false;


    //private float time_remain;
    
    //"times up! pops up when the player has already pressed a choice button.
    //instead of freezing time we can make the rewind time slower

    void Start()
    {
        timeremain = maxtime; //initializes timeremain (tracks how much time remaining) to the value of maxtime (max duration for the countdown)

       //coroutine allows you to run code over time without freezing the game
       //this decreases timeremain until it reaches zero. activates time's up thereafter
        StartCoroutine(RunTimer()); 
        
    }

    private IEnumerator RunTimer()
    {
        while (timeremain > 0) //loops until the timer reaches zero
        {
            timeremain -= Time.deltaTime; //decreases the remaining time by the amount of time passed 
            timerlinear.fillAmount = timeremain / maxtime; //updates the slider (linear bar)
            yield return null; 
        }
            
        dialogue_Manager.choiceContainer.gameObject.SetActive(false); //disables the choice container
        
    }

    public void RewindTime()
    {
        if (!rewindUsed)
        {
            rewindUsed = true;

            if (rewindSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(rewindSFX);
            }
            StartCoroutine(ReloadSceneAfterDelay(0.5f));

          
        }
        else
        {
            rewind_btn.interactable = false;
            continue_btn.SetActive(true);
        }
    }

    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Skip()
    {
        rewindUsed = true;
        int current_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_index + 1);
    }

    
    public void NextDialogueScene(string sceneName)
    {
        //next scene after pressing button
        SceneManager.LoadScene(sceneName); 
    }
}
