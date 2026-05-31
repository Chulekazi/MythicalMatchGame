using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer3 : MonoBehaviour
{
    public separateManager dialogue_Manager;
    public Button rewind_btn; 
    public AudioClip rewindSFX;
    public AudioSource audioSource;
    public Image timerlinear;
    public Dialogue dialogue;
    public Button proceed;
    public TMP_Text dialogue_text; 

    float timeremain;
    public float maxtime = 5.0f;

    public static int rewind_count = 0;
    public int max_rewind = 2;

    void Start()
    {
        
        timeremain = maxtime; 
        StartCoroutine(RunTimer());

    }

    private IEnumerator RunTimer()
    {
        while (timeremain > 0) 
        {
            timeremain -= Time.deltaTime; //decreases the remaining time by the amount of time passed 
            timerlinear.fillAmount = timeremain / maxtime; //updates the slider (linear bar)
            yield return null;
        }

        dialogue_Manager.choiceContainer.gameObject.SetActive(false); //disables the choice container

    }

    public void RewindTime()
    {
        if (rewind_count < max_rewind)
        {
            rewind_count++;
            if (rewindSFX != null && audioSource != null)
            { audioSource.PlayOneShot(rewindSFX); }

            StartCoroutine(ReloadSceneAfterDelay(0.5f));
        }
        else
        {
            rewind_btn.interactable = false;
            proceed.gameObject.SetActive(true);
        }
        
    }

    IEnumerator ReloadSceneAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

public void NextDialogueScene(string sceneName)
    {
        //next scene after pressing button
        SceneManager.LoadScene(sceneName);
    }
}
