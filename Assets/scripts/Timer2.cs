using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer2 : MonoBehaviour
{
    public DialogueManager manager_;
    public Button rewind_;
    public Image linear_;
    public TMP_Text text_;
    float time_;
    public float maxtime_ = 5.0f;
    public static bool rewindUsed_ = false;

    void Start()
    {
        time_ = maxtime_;
        StartCoroutine(RunTimer_());
    }

    private IEnumerator RunTimer_()
    {
        while (time_ > 0) //loops until the timer reaches zero
        {
            time_ -= Time.deltaTime; //decreases the remaining time by the amount of time passed 
            linear_.fillAmount = time_ / maxtime_; //updates the slider (linear bar)
            yield return null;
        }

        manager_.choiceContainer.gameObject.SetActive(false); //disables the choice container

    }

    public void RewindTime_()
    {
        if (!rewindUsed_) // if player has used rewind:
        {
            rewindUsed_ = true; //mark as true
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reload the scene, starts the timer again
        }
        else
        {
            rewind_.interactable = false; //rewind button is not interactable
            text_.gameObject.SetActive(false); // dialogue text is inactive
        }

    }
}
