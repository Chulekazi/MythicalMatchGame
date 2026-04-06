using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public DialogueData dialogue_options;
    public Button[] optionButtons;

    public TMP_Text dialogue_text;
    public TMP_Text character_text;
    public List<Game> dialogue;

    private int line_index = 0;

    public Button continue_button;
    public Button next_scene;
    public Button pause_screen;

    public GameObject journal;
    

    void Start()
    {
        Display_Line();
        continue_button.onClick.AddListener(Next_Line);
        next_scene.gameObject.SetActive(false);

        foreach (Button btn in optionButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void Display_Line()
    {
        if(line_index<dialogue.Count)
        {
            string charName = dialogue[line_index].char_name;

            if(charName == "characterText")
            {
                charName = PlayerData.playerName;
            }

            character_text.text = charName;
            dialogue_text.text = dialogue[line_index].dialogue_text.Replace("{playerName}", PlayerData.playerName);

            if(line_index == 7 )
            {
                ShowDialogueOption();
                continue_button.gameObject.SetActive(false);
            }
        }
        else
        {
            End_Dialogue();
        }
    }

    public void Next_Line()
    {

        line_index++;
        Display_Line();
    }

    public void ShowDialogueOption()
    {
        for (int i = 0; i < optionButtons.Length; i++) 
        {
            if (i < dialogue_options.dialoguestring.Length)
            {
                //display dialogue option
                optionButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI textComponent = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                textComponent.text = dialogue_options.dialoguestring[i];

                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(index));
            }
            else
            {
                optionButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SelectOption(int optionIndex)
    {
        if(optionIndex >= 0 && optionIndex < dialogue_options.dialoguestring.Length)
        {
            Debug.Log("Player chose: " + dialogue_options.dialoguestring [optionIndex]);

            foreach (Button btn in optionButtons)
            {
                btn.gameObject.SetActive(false);
            }

            line_index++;
            Display_Line();
            continue_button.gameObject.SetActive(true);
        }
    }

    void End_Dialogue()
    {
        dialogue_text.text = "";
        character_text.text = "";
        continue_button.gameObject.SetActive(false);
        next_scene.gameObject.SetActive(true);

        foreach (Button btn in optionButtons)
        {
            btn.gameObject.SetActive(false); 
        }

    }

    public void ShowJournal()
    {
        journal.SetActive(true);
        continue_button.gameObject.SetActive(false);
    }

    public void HideJournal()
    {
        journal.SetActive(false);
        continue_button.gameObject.SetActive(true);
    }

    public void PauseButton()
    {
        pause_screen.gameObject.SetActive(true);
    }

    public void PlayButton()
    {
        pause_screen.gameObject.SetActive(false);
    }
}
