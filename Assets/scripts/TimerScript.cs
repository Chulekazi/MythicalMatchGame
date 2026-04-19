using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public DialogueManager dialogue_Manager;

    public GameObject continue_btn; //button to display next dialogue line
    public GameObject message; //"player can't rewind time" text

    public Button rewind_btn; //rewind time button

    public Image timerlinear; //slider

    public TMP_Text dialogue_text; //dialogue text, mainly for the quiz questions
    public TMP_Text game_message;
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
        game_message.gameObject.SetActive(true);
    }

    public void RewindTime()
    {
        if(!rewindUsed) // if player hasn't used rewind yet:
        {
            rewindUsed = true; //mark as true
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload the scene, starts the timer again
            //continue btn
        }
        else
        {
            rewind_btn.interactable = false; //rewind button is not interactable
            dialogue_text.gameObject.SetActive(false); // dialogue text is inactive

            continue_btn.SetActive(true);
            //player presses continue button to next scene gah lee
            message.SetActive(true); //text message
            game_message.gameObject.SetActive(false);
            //audio sfx 
        }
       
    }

    
    public void NextDialogueScene(string sceneName)
    {
        //next scene after pressing button
        SceneManager.LoadScene(sceneName); 
    }
}
