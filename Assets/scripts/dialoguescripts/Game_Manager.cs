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
    public string[] dialogue_options;
    public Button[] optionButtons;

    public TMP_Text dialogue_text;
    public TMP_Text character_text;
    public List<Game> dialogue;

    public int line_index = 0;

    public Button continue_button;
    public Button next_scene;
    public GameObject pause_screen;

    public GameObject journal;
    public GameObject settings;
    public Image_Enabler image_enabler_;
   

    void Start() 
    {
        dialogue_options = new string[] { "You can do tha-", "No. I'm terrified. Who are you and why are you in my ho-", "Uhh okay."};
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
            
            //if dialogue line reaches line  it must trigger showdialogueoption function (honestly i want to make this any number but the code is working lol)
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
        image_enabler_.CheckLineIndex(line_index);
    }

    public void ShowDialogueOption()
    {
        for (int i = 0; i < optionButtons.Length; i++)
        {
            if (i < dialogue_options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI textComponent = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                textComponent.text = dialogue_options[i];

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
            if(i < dialogue_options.Length)
            {
                optionButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI textcomponent = optionButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                textcomponent.text = dialogue_options[i];

                int index = i;
                optionButtons[i].onClick.RemoveAllListeners();
                optionButtons[i].onClick.AddListener(() => SelectOption(index));
            }
        }
    }

    public void SelectOption(int optionIndex)
    {
        if(optionIndex >= 0 && optionIndex < dialogue_options.Length)
        {
            Debug.Log("Player chose: " + dialogue_options[optionIndex]);

            foreach (Button btn in optionButtons)
            {
                btn.gameObject.SetActive(false);
            }

            line_index++;
            Display_Line();
            continue_button.gameObject.SetActive(true);
        }
    }


    public void ShowSettingsPanel()
    {
        settings.gameObject.SetActive(true);
    }
    public void HideSettingsPanel()
    {
        settings.gameObject.SetActive(false);
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

   public void SaveGameProgress()
    {
        int currIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currIndex);
        PlayerPrefs.Save();
    }

    public void LoadGameProgress()
    {
        if(PlayerPrefs.HasKey("SavedScene"))
        {
            int savedIndex = PlayerPrefs.GetInt("SavedScene");
            SceneManager.LoadScene(savedIndex);
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
