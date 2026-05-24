using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BIGMANAGER : MonoBehaviour
{
    public TMP_Text npc;
    public Transform options_parent;
    public Button options_button;
    public Button next;
    public Button skip;

    public int heartpoints = 0;
    public GameObject journal_panel;
    public TMP_Text heartpoints_text;
    public List<string> correct_answer = new List<string>();
    public TMP_Text history_log;
    public Rewind_button rewind; //other script

    private Dialogue_line current_line;
    private HashSet<dialogue_optionSO> selected_options = new HashSet<dialogue_optionSO>();
    private int maxChoices = 2;
    private bool show_response = false;
    public GameObject name_panel;
    public TMP_Text speaker_text;
    public Image character_image;

    public Image background_panel;
    public List<Sprite> background_sprites;
    public List<string> background_names;
    public TooManyTimers dialogue_timer; //different script
    public AudioSource audioSource;
    public AudioClip nextButtonSound;

    void Start()
    {
        dialogue_timer.OnTimerFinished += HandleTimeout;
    }

    void HandleTimeout()
    {
        Debug.Log("Time ran out! Auto-selecting...");

        if (current_line.optionSOs != null && current_line.optionSOs.Length > 0)
        {
            SelectOption(current_line.optionSOs[0]); // auto-pick fallback
        }
    }

    public void Start_Dialogue(Dialogue_line start_line)
    {
        current_line = start_line;
        skip.onClick.RemoveAllListeners();
        skip.onClick.AddListener(() => {
            if (current_line.Next_Line != null)
            {
                current_line = current_line.Next_Line;
                selected_options.Clear();
                show_response = false;
                DisplayCurrentLine();
            }
        });
        DisplayCurrentLine();
    }

    public void Name_Check(Dialogue_line start_line)
    {
        if (string.IsNullOrEmpty(Player_profile.Instance.player_name))
        {
            name_panel.SetActive(true);
        }
        else
        {
            Start_Dialogue(start_line);
        }
    }

    void DisplayCurrentLine()
    {
        if (!show_response)
        {
            npc.text = current_line.npc_text.Replace("{player}", Player_profile.Instance.player_name);
            speaker_text.text = current_line.npc_name.ToLower() == "player"
                ? Player_profile.Instance.player_name
                : current_line.npc_name;

            if (current_line.speaker_ != null)
            {
                character_image.gameObject.SetActive(true);
                character_image.sprite = current_line.speaker_;
                character_image.rectTransform.localScale = current_line.spriteScale;
            }
            else
            {
                character_image.gameObject.SetActive(false);
            }

            if (current_line.background != null)
            {
                background_panel.sprite = current_line.background.background_sprite;
            }
        }

        foreach (Transform child in options_parent)
        {
            Destroy(child.gameObject);
        }

        var availableOptions = current_line.optionSOs
            .Where(o => !selected_options.Contains(o)).ToArray();

        if (availableOptions.Length > 0 && selected_options.Count < maxChoices)
        {
            options_parent.gameObject.SetActive(true);
            dialogue_timer.Start_Timer();
            dialogue_timer.OnTimerFinished += HandleTimeout;
            next.gameObject.SetActive(false);

            rewind.Reset_Rewind();

            foreach (var option in availableOptions)
            {
                Button new_button = Instantiate(options_button, options_parent);
                new_button.GetComponentInChildren<TMP_Text>().text = option.option_;
                new_button.onClick.AddListener(() => SelectOption(option));
            }
        }
        else
        {
            options_parent.gameObject.SetActive(false);
            next.gameObject.SetActive(true);

            next.onClick.RemoveAllListeners();
            next.GetComponentInChildren<TMP_Text>().text = "continue";
            next.onClick.AddListener(() => {
                PlayNextSound(); 
                SelectNextLine();
            });
        }
    }

    void SelectNextLine()
    {
        if (current_line.Next_Line != null)
        {
            current_line = current_line.Next_Line;
            selected_options.Clear();
            show_response = false;
            DisplayCurrentLine();
        }
    }

    void SelectOption(dialogue_optionSO option)
    {
        npc.text = option.response_;
        speaker_text.text = Player_profile.Instance.player_name;
        selected_options.Add(option);
        show_response = true;
        dialogue_timer.Stop_Timer();

        if (option.Is_correct)
        {
            heartpoints++;
            correct_answer.Add(option.option_);
            update_heartpoints();
        }

        next.gameObject.SetActive(true);
        options_parent.gameObject.SetActive(false);

        Object.FindFirstObjectByType<Rewind_button>()?.EnableRewind(option);

        next.onClick.RemoveAllListeners();
        next.onClick.AddListener(() => {
            PlayNextSound();
            show_response = false;
            if (option.next_line_ != null)
            {
                current_line = option.next_line_;
                selected_options.Clear();
            }
            DisplayCurrentLine();
        });
    }

    public void open_journal()
    {
        journal_panel.SetActive(!journal_panel.activeSelf);
        update_heartpoints();
    }

    public void update_heartpoints()
    {
        heartpoints_text.text = "Heart Points: " + heartpoints;
        history_log.text = "Correct Answer:\n";
        foreach (string answer in correct_answer)
        {
            history_log.text += "- " + answer + "\n";
        }
    }

    public void Rewind(dialogue_optionSO option)
    {
        show_response = false;
        if (selected_options.Count >= maxChoices)
        {
            SelectNextLine();
        }
        else
        {
            DisplayCurrentLine();
        }
        rewind.rewind.interactable = false;
    }

    public void EnableRewind(dialogue_optionSO option)
    {
        rewind.rewind.interactable = true;
    }

    public void DisableRewind()
    {
        rewind.rewind.interactable = false;
        StopAllCoroutines();
    }

    void ResetChoices()
    {
        selected_options.Clear();
        DisplayCurrentLine();
    }

    
    void PlayNextSound()
    {
        if (audioSource != null && nextButtonSound != null)
        {
            audioSource.PlayOneShot(nextButtonSound);
        }
    }
}