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
    public AudioClip rewindSFX;
    public AudioSource audioSource;

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
        if (!rewindUsed_)
        {
            rewindUsed_ = true;

            if (rewindSFX != null && audioSource != null)
            {
                audioSource.PlayOneShot(rewindSFX);
            }
            StartCoroutine(ReloadSceneAfterDelay(0.5f));


        }
        else
        {
            rewind_.interactable = false; //rewind button is not interactable
            text_.gameObject.SetActive(false); // dialogue text is inactive
        }
        IEnumerator ReloadSceneAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
