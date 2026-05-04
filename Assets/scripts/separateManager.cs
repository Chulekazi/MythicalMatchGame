using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine.SceneManagement;
using System.IO;

public class separateManager : MonoBehaviour
{

    public TMP_Text dialogue;
    //public TMP_Text heartpointsText;


    public Image portrait;

    public Transform choiceContainer;

    public GameObject choiceButtonPrefab;
    public GameObject dialogueBox;
    public GameObject pause_screen;
    public GameObject journal_screen;
    public Button continue_3;
    public Button continue_1;
    public Button continue_2;
    public Button continue_4;
    public Timer3 timer;

    


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
            //btn
            EndDialogue();
            return;
        }
        Dialogue line = lines.Dequeue();
        string processedText = line.dialogueText.Replace("player's name", PlayerData.playerName);
        dialogue.text = processedText;
        dialogue.text = line.dialogueText;
        portrait.sprite = line.image;

        DisplayContinue_button(line);

        if (line.choices != null && line.choices.Count > 0)
        {
            choiceContainer.gameObject.SetActive(true);

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


                    // PlayerData.playerHeartPoints += choice.heartpoints;
                    //Debug.Log("heart points: " + PlayerData.playerHeartPoints);

                    BeginDialogue(choice.nextLine);


                    //UpdatePointsUI();
                });
            }
        }
    }

    public void DisplayContinue_button(Dialogue dialogue)
    {
        continue_1.gameObject.SetActive(false);
        continue_2.gameObject.SetActive(false);
        continue_3.gameObject.SetActive(false);
        continue_4.gameObject.SetActive(false);
        if(dialogue.dialogueText == "Okay, so, I have sooo many siblings, but we all have the same mom and dad! I’m the youngest out of all of us so I get to just run around and do whatever I like all day.")
        {
            continue_1.gameObject.SetActive(true);
        }
        else if (dialogue.dialogueText == "I mean, there’s so much to do down here! I like to go up to the surface and watch the humans though. I have a little box of trinkets somewhere in here.")
        {
            continue_2.gameObject.SetActive(true);
        }
        else if ( dialogue.dialogueText == "You’re in my room in my family’s palace.")
        {
            continue_3.gameObject.SetActive(true);
        }
        else if (dialogue.dialogueText == "Oh yay! I love choosing!")
        {
            continue_4.gameObject.SetActive(true);
        }
    }

    public void Pause_Button()
    {
        pause_screen.gameObject.SetActive(true);
    }

    public void Play_Button()
    {
        pause_screen.gameObject.SetActive(false);
    }

    public void Open_Journal()
    {
        journal_screen.gameObject.SetActive(true);
    }

    public void Close_Journal()
    {
        journal_screen.gameObject.SetActive(false);
    }

    void ClearChoices()
    {
        foreach (Transform child in choiceContainer)
        {
            Destroy(child.gameObject);
        }
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