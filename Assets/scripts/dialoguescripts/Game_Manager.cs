using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager : MonoBehaviour
{
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
    }

    public void Display_Line()
    {
        if (line_index < dialogue.Count)
        {
            character_text.text = dialogue[line_index].char_name;
            dialogue_text.text = dialogue[line_index].dialogue_text;
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

    void End_Dialogue()
    {
        dialogue_text.text = "";
        character_text.text = "";
        continue_button.gameObject.SetActive(false);
        next_scene.gameObject.SetActive(true);

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
