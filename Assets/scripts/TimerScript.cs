using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public DialogueManager dialogue_Manager;

    public GameObject timesup;
    public GameObject continue_btn;

    public Image timerlinear;

    float timeremain;
    public float maxtime = 5.0f;

    //private float time_remain;

    private bool isFrozen = false;
    
    
    //"times up! pops up when the player has already pressed a choice button.

    void Start()
    {
        timeremain = maxtime;
        timesup.SetActive(false);
    }

    void Update()
    {
        if (!isFrozen)
        {
            if (timeremain > 0)
            {
                timeremain -= Time.deltaTime;
                timerlinear.fillAmount = timeremain / maxtime;

            }
            else
            {
                timesup.SetActive(true);
                dialogue_Manager.choiceContainer.gameObject.SetActive(false);
                continue_btn.SetActive(true);
                // set freeze btn inactive
                
            }
        }
    }

      public void Freeze_Time()
    {
        StartCoroutine(FreezeCoroutine());
    }

    private System.Collections.IEnumerator FreezeCoroutine()
    {
        isFrozen = true;
        yield return new WaitForSeconds(5f);
        isFrozen = false;
    }
}
