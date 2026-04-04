using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    
    public TMP_Text dialogue;
    //public TMP_Text heartpointsText;
    

    public Image portrait;

    public Transform choiceContainer;

    public GameObject choiceButtonPrefab;
    public GameObject dialogueBox;
    public GameObject book;

    public TimerScript timer;

    
    //public bool endTimer;

    
    private Queue<Dialogue> lines = new Queue<Dialogue>();

    public void BeginDialogue(List<Dialogue> dialoguelines)
    {
        lines.Clear();
        foreach (var line in dialoguelines)
        {
            lines.Enqueue(line);
        }
        DisplayDialogueLine();
    }

    public void DisplayDialogueLine()
    {
        ClearChoices();

        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        Dialogue line = lines.Dequeue();
        
        string processedText = line.dialogueText.Replace("player's name", PlayerData.playerName);
        dialogue.text = processedText;
        dialogue.text = line.dialogueText;
        portrait.sprite = line.image;

        if (line.choices != null && line.choices.Count > 0)
        {   choiceContainer.gameObject.SetActive(true);

            foreach (var choice in line.choices) //foreach variable choice in line choices we want to instantiate the choice button prefab
            {
                GameObject buttonobject = Instantiate(choiceButtonPrefab, choiceContainer);
                TMP_Text buttontext = buttonobject.GetComponentInChildren<TMP_Text>();
                buttontext.text = choice.quizAnswer;

                buttonobject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    choiceContainer.gameObject.SetActive(false);
                    timer.timerlinear.gameObject.SetActive(false);
                    //bool timertextoff
                    
                    
                    PlayerData.playerHeartPoints += choice.heartpoints;
                    Debug.Log("heart points: " + PlayerData.playerHeartPoints);

                    BeginDialogue(choice.nextLine);

                    //UpdatePointsUI();
                });
            }
        }
    }

    void ClearChoices()
    {
        foreach(Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
    }

    public void OpenJournal()
    {
        book.SetActive(true);
    }

    public void CloseJournal()
    {
        book.SetActive(false);
    }

   /* void UpdatePointsUI()
    {
        heartpointsText.text = "Your heart points: " + PlayerData.playerHeartPoints;
    }
   */
    void EndDialogue()
    {   
        //hides the dialogue box and choice container
        dialogueBox.SetActive(false);
        choiceContainer.gameObject.SetActive(false);
        
        Debug.Log("Dialogue ended.");
    }
}