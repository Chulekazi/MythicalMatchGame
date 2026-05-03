using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class THEdialoguemanager : MonoBehaviour
{
    public static THEdialoguemanager Instance { get; private set; }

    public TMP_Text dialogue_text;
    public TMP_Text character_text;
    public Image image;
    public GameObject dialogue_box;

    public Transform choice_container;
    public GameObject choicebutton_prefab;
    public string[] dialogue_options;
    public Button[] option_buttons;

    public Button continue_button;
    public Button nextscene_button;

    public GameObject pause_screen;
    public GameObject journal_screen;

    private Queue<Dialogue> lines = new Queue<Dialogue>();
    public int line_index = 0;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        nextscene_button.gameObject.SetActive(false);
        foreach(Button btn in option_buttons)
        {
            btn.gameObject.SetActive(false);
        }
        continue_button.onClick.AddListener(Next_Line);
    }
    public void Next_Line()
    {
        line_index++;
        DisplayDialogueLine();
    }

    public void Begin_Dialogue(List<Dialogue> dialogue_lines)
    {
        lines.Clear();
        foreach(var line in dialogue_lines)
        {
            lines.Enqueue(line);
        }
        DisplayDialogueLine();
    }

    public void DisplayDialogueLine()
    {
        Clear_choices();
        if(lines.Count ==0)
        {
            End_Dialogue();
            return;
        }
        Dialogue line = lines.Dequeue();
        string processed_text = line.dialogueText.Replace("{playerName}", PlayerData.playerName);
        dialogue_text.text = processed_text;
        character_text.text = line.characterName;
        image.sprite = line.image;
        if(line.choices != null && line.choices.Count > 0)
        {
            choice_container.gameObject.SetActive(true);
            continue_button.gameObject.SetActive(false);
            foreach(var choice in line.choices)
            {
                GameObject button_obj = Instantiate(choicebutton_prefab, choice_container);
                TMP_Text button_text = button_obj.GetComponentInChildren<TMP_Text>();
                button_text.text = choice.quizAnswer;

                button_obj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    choice_container.gameObject.SetActive(false);
                    Begin_Dialogue(choice.nextLine);
                    continue_button.gameObject.SetActive(true);
                });
            }
        }
        else
        {
            continue_button.gameObject.SetActive(true);
        }
    }
    void Clear_choices()
    {
        foreach(Transform child in choice_container)
        {
            Destroy(child.gameObject);
        }
    }

    void End_Dialogue()
    {
        dialogue_box.SetActive(false);
        choice_container.gameObject.SetActive(false);
        continue_button.gameObject.SetActive(false);
        nextscene_button.gameObject.SetActive(true);
        Debug.Log("dialogue ended.");
    }

    public void Pause_Button()
    {
        pause_screen.SetActive(true);
    }
    public void Play_Button()
    {
        pause_screen.SetActive(false);
    }
    public void Open_Journal()
    {
        journal_screen.SetActive(true);
    }
    public void Close_Journal()
    {
        journal_screen.SetActive(false);
    }
}


