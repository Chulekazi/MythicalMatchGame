using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class onemoreManager : MonoBehaviour
{
    public TMP_Text dialogue_;
    //public TMP_Text heartpointsText;


    public Image portrait_;

    public Transform choiceContainer_;

    public GameObject choiceButtonPrefab_;
    public GameObject dialogueBox_;
    public GameObject pause_screen_;
    public GameObject journal_screen_;
    public Button continue3;
    public Button continue1;
    public Button continue2;
    public Button continue4;
    public Timer3 timer_;




    private Queue<Dialogue> lines_ = new Queue<Dialogue>();


    public void BeginDialogue(List<Dialogue> dialoguelines)
    {
        lines_.Clear();
        foreach (var line in dialoguelines)
        {
            lines_.Enqueue(line);
        }
        DisplayDialogueLine();
    }

    public void DisplayDialogueLine()
    {
        ClearChoices();

        if (lines_.Count == 0)
        {
            //btn
            EndDialogue();
            return;
        }
        Dialogue line = lines_.Dequeue();
        string processedText = line.dialogueText.Replace("player's name", PlayerData.playerName);
        dialogue_.text = processedText;
        dialogue_.text = line.dialogueText;
        portrait_.sprite = line.image;

        DisplayContinue_button(line);

        if (line.choices != null && line.choices.Count > 0)
        {
            choiceContainer_.gameObject.SetActive(true);

            foreach (var choice in line.choices) //foreach variable choice in line choices we want to instantiate the choice button prefab
            {
                GameObject buttonobject = Instantiate(choiceButtonPrefab_, choiceContainer_);
                TMP_Text buttontext = buttonobject.GetComponentInChildren<TMP_Text>();
                buttontext.text = choice.quizAnswer;

                buttonobject.GetComponent<Button>().onClick.AddListener(() =>
                {
                    choiceContainer_.gameObject.SetActive(false);
                    timer_.timerlinear.gameObject.SetActive(false);
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
        continue1.gameObject.SetActive(false);
        continue2.gameObject.SetActive(false);
        continue3.gameObject.SetActive(false);
        continue4.gameObject.SetActive(false);
        if (dialogue.dialogueText == "I…uh…sorry, I didn’t mean to make you upset.")
        {
            continue1.gameObject.SetActive(true);
        }
        else if (dialogue.dialogueText == "I like how straightforward you are!")
        {
            continue2.gameObject.SetActive(true);
        }
        else if (dialogue.dialogueText == "Was that the wrong answer?")
        {
            continue3.gameObject.SetActive(true);
        }
        else if (dialogue.dialogueText == "I’m sorry! I didn’t mean for that to be mean!")
        {
            continue4.gameObject.SetActive(true);
        }
    }

    public void Pause_Button()
    {
        pause_screen_.gameObject.SetActive(true);
    }

    public void Play_Button()
    {
        pause_screen_.gameObject.SetActive(false);
    }

    public void Open_Journal()
    {
        journal_screen_.gameObject.SetActive(true);
    }

    public void Close_Journal()
    {
        journal_screen_.gameObject.SetActive(false);
    }

    void ClearChoices()
    {
        foreach (Transform child in choiceContainer_)
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
        dialogueBox_.SetActive(false);
        choiceContainer_.gameObject.SetActive(false);

        Debug.Log("Dialogue ended.");
    }
}
