using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class DialogueManager : MonoBehaviour
{
    
    public TMP_Text dialogue;
    public TMP_Text points_text;
    public AudioClip choiceClickSound;

    public Image portrait;

    public Transform choiceContainer;

    public GameObject choiceButtonPrefab;
    public GameObject dialogueBox;
    
    public GameObject pause_screen;
    public GameObject journal_screen;
    public string[] journal_text;
    public TMP_Text journaltext_;

    public TimerScript timer;
    public AudioSource audioSource;


    private Queue<Dialogue> lines = new Queue<Dialogue>();

    void Awake()
    {
        Load_PlayerData();
    }

    void OnApplicationQuit()
    {
        Save_PlayerData();
    }

    public void Next_Scene()
    {
        Save_PlayerData();
        int curr_index = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curr_index + 1); 
    }
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
        portrait.sprite = line.image;

        if (line.choices != null && line.choices.Count > 0)
        {
            choiceContainer.gameObject.SetActive(true);

            for (int i = 0; i < line.choices.Count; i++)
            {
                var choice = line.choices[i];
                string choiceId = line.dialogueText + "_" + i;

                GameObject buttonobject = Instantiate(choiceButtonPrefab, choiceContainer);
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

                    choiceContainer.gameObject.SetActive(false);
                    timer.timerlinear.gameObject.SetActive(false);

        
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

    public void HandleChoice(DialogueChoice choice)
    {
        if (choice.heartpoints == 0 && choice.sound != null)
        {
            audioSource.PlayOneShot(choice.sound);
        }
        BeginDialogue(choice.nextLine);
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
        foreach(Transform child in choiceContainer)
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