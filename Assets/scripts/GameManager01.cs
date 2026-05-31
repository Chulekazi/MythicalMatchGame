using System.Collections.Generic;
using System.IO;
using System.Collections;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager01 : MonoBehaviour
{
   

    public TMP_Text dialogue_text01;
    public TMP_Text character_text01;

    public List<Game> dialogue01;

    public int line_index01 = 0;

    public Button continue_button01;
    public Button next_scene01;

    public GameObject pause_screen01;
    public GameObject journal01;

    public Image_Enabler image_enabler;


    void Start()
    {
        Display_Line01(); //calls display line function
        //continue_button.onClick.AddListener(Next_Line);

        //hide next scene button unitl dialogue is complete
        next_scene01.gameObject.SetActive(false);
    }

    public void Display_Line01()
    {

        // if function - current line index is still within the dialogue list, else call end dialogue function
        if (line_index01 < dialogue01.Count)
        {
            //retrieve character name in place of "characterText". if placeholder is used, it substitutes the player's name from PlayerData
            string charName = dialogue01[line_index01].char_name;

            if (charName == "characterText")
            {
                charName = PlayerData.playerName;
            }

            //display character/player name in UI
            character_text01.text = charName;
            //show dialogue line of placeholder name with player name
            dialogue_text01.text = dialogue01[line_index01].dialogue_text.Replace("{playerName}", PlayerData.playerName);

            //if dialogue line reaches line 10 it must trigger showdialogueoption function (honestly i want to make this any number but the code is working lol)
            if (line_index01 == 24)
            {
                continue_button01.gameObject.SetActive(false);
            }
        }
        else
        {
            End_Dialogue01();
        }
    }

    public void Next_Line01()
    {
        line_index01++;
        Display_Line01();
        image_enabler.CheckLineIndex(line_index01);
    }

    void End_Dialogue01()
    {
        dialogue_text01.text = "";
        character_text01.text = "";
        continue_button01.gameObject.SetActive(false);
        next_scene01.gameObject.SetActive(true);

    }

    public void ShowJournal01()
    {
        journal01.SetActive(true);
        continue_button01.gameObject.SetActive(false);
    }

    public void HideJournal01()
    {
        journal01.SetActive(false);
        continue_button01.gameObject.SetActive(true);
    }

    public void PauseButton01()
    {
        pause_screen01.gameObject.SetActive(true);
    }

    public void PlayButton01()
    {
        pause_screen01.gameObject.SetActive(false);
    }

    public void SaveGameProgress()
    {
        int currIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", currIndex);
        PlayerPrefs.Save();
    }

    public void LoadGameProgress()
    {
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            int savedIndex = PlayerPrefs.GetInt("SavedScene");
            SceneManager.LoadScene(savedIndex);
        }

    }
}
