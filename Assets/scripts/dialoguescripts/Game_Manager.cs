using System.Collections.Generic;
using System.IO;
using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
    public DialogueData dialogue_options;
    public Button[] optionButtons;

    public TMP_Text dialogue_text;
    public TMP_Text character_text;
    public List<Game> dialogue;

    public int line_index = 0;

    public Button continue_button;
    public Button next_scene;
    public GameObject pause_screen;

    public GameObject journal;

    void Start() 
    {
        Display_Line(); //calls display line function
        //continue_button.onClick.AddListener(Next_Line);

        //hide next scene button unitl dialogue is complete
        next_scene.gameObject.SetActive(false); 

        //loop through all dialogue choice buttons in optionButtons
        foreach (Button btn in optionButtons)
        {
            //hide four buttons until game manager shows them 
            btn.gameObject.SetActive(false);
        }

    }

    public void Display_Line()
    {
       
        // if function - current line index is still within the dialogue list, else call end dialogue function
        if(line_index<dialogue.Count)
        {
            //retrieve character name in place of "characterText". if placeholder is used, it substitutes the player's name from PlayerData
            string charName = dialogue[line_index].char_name;

            if(charName == "characterText")
            {
                charName = PlayerData.playerName;
            }

            //display character/player name in UI
            character_text.text = charName;
            //show dialogue line of placeholder name with player name
            dialogue_text.text = dialogue[line_index].dialogue_text.Replace("{playerName}", PlayerData.playerName);
            
            //if dialogue line reaches line 7 it must trigger showdialogueoption function (honestly i want to make this any number but the code is working lol)
            if(line_index == 10 )
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

    public void ShowDialogue(string dialogueText01)
    {
        dialogue_text.text = dialogueText01;
        foreach (var button in optionButtons)
        {
            button.gameObject.SetActive(false);
        }
        StartCoroutine(ShowGameOptions(1f));
    }

    private IEnumerator ShowGameOptions(float delay)
    {
        yield return new WaitForSeconds(delay);
        for(int i = 0; i < optionButtons.Length; i++)
        {
            if(i < dialogue_options.dialoguestring.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI textcomponent = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                textcomponent.text = dialogue_options.dialoguestring[i];

                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(index));
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
            //remove all options from scene
            btn.gameObject.SetActive(false); 
        }
        
    }

   /* public void Save_Game()
    {
        SaveData data = new SaveData();
        data.scene = SceneManager.GetActiveScene().name;
        data.dialogueProgress = line_index;
        data.playerName = PlayerData.playerName;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/save.json", json);
        Debug.Log("Game Saved: " + json);
    }


    public void Load_Game()
    {
        string path = Application.persistentDataPath + "save.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            line_index = data.dialogueProgress;
            PlayerData.playerName = data.playerName;

            if(SceneManager.GetActiveScene().name != data.scene)
            {
                SceneManager.LoadScene(data.scene);
            }
            else
            {
                Display_Line();
            }
        }
    }*/

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
