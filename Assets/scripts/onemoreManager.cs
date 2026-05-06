using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class onemoreManager : MonoBehaviour
{
    public TMP_Text dialogue_;
    public TMP_Text points_text;
    public TMP_Text journaltext_;

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
    public Timer4 timer_;

    public AudioSource audioSource;
    public AudioClip choiceClickSound;


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
            EndDialogue();
            return;
        }

        Dialogue line = lines_.Dequeue();
        string processedText = line.dialogueText.Replace("player's name", PlayerData.playerName);
        dialogue_.text = processedText;
        portrait_.sprite = line.image;

        if (line.choices != null && line.choices.Count > 0)
        {
            choiceContainer_.gameObject.SetActive(true);

            for (int i = 0; i < line.choices.Count; i++)
            {
                var choice = line.choices[i];
                string choiceId = line.dialogueText + "_" + i;

                GameObject buttonobject = Instantiate(choiceButtonPrefab_, choiceContainer_);
                TMP_Text buttontext = buttonobject.GetComponentInChildren<TMP_Text>();
                buttontext.text = choice.quizAnswer;

                Button btn = buttonobject.GetComponent<Button>();
                if (PlayerData.clicked_.Contains(choiceId))
                {
                    btn.interactable = false;
                }

                btn.onClick.AddListener(() =>
                {
                    btn.interactable = false;
                    PlayerData.clicked_.Add(choiceId);

                    choiceContainer_.gameObject.SetActive(false);
                    timer_.timerlinear.gameObject.SetActive(false);


                    if (choiceClickSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(choiceClickSound);
                    }

                    if (choice.heartpoints > 0)
                    {
                        PlayerData.PlayerHeartPoints += choice.heartpoints;
                        DisplayPoints();
                        DisplayTextJournal(choice.journal_entry);
                    }

                    BeginDialogue(choice.nextLine);
                });
            }
        }
    }

    public void DisplayTextJournal(string entry_)
    {

        if (!journaltext_.text.Contains(entry_))
        {
            journaltext_.text += "\n" + entry_;
        }
        journaltext_.text = string.Join("\n", PlayerData.JournalEntries);
    }

    public void DisplayPoints()
    {
        points_text.text = "Points: " + PlayerData.PlayerHeartPoints;
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

    public void Save_PlayerData()
    {
        PlayerPrefs.SetInt("HeartPoints", PlayerData.PlayerHeartPoints);
        PlayerPrefs.SetString("PlayerName", PlayerData.playerName);
        PlayerPrefs.SetString("ClickedChoices", string.Join(",", PlayerData.clicked_));
        PlayerPrefs.SetString("JournalEntries", string.Join("|", PlayerData.JournalEntries));
        PlayerPrefs.Save();
    }

    public void Load_PlayerData()
    {
        if (PlayerPrefs.HasKey("HeartPoints"))
        {
            PlayerData.PlayerHeartPoints = PlayerPrefs.GetInt("HeartPoints");
            PlayerData.playerName = PlayerPrefs.GetString("PlayerName");

            string clicked = PlayerPrefs.GetString("ClickedChoices", "");
            PlayerData.clicked_ = string.IsNullOrEmpty(clicked)
                ? new HashSet<string>()
                : new HashSet<string>(clicked.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

            string journal = PlayerPrefs.GetString("JournalEntries", "");
            PlayerData.JournalEntries = string.IsNullOrEmpty(journal)
                ? new List<string>()
                : new List<string>(journal.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries));
        }
        DisplayPoints();
    }
}
