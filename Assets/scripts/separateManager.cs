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
    public TMP_Text points_text;
    public TMP_Text journaltext_;



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

            string cliked = PlayerPrefs.GetString("ClickedChoices");
            PlayerData.clicked_ = new HashSet<string>(cliked.Split(','));

            string journal = PlayerPrefs.GetString("JournalEntries", "");
            if (!string.IsNullOrEmpty(journal))
            {
                PlayerData.JournalEntries = new List<string>(journal.Split('|'));
            }
        }
        DisplayPoints();
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
            //continue btn
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