using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class BIGMANAGER : MonoBehaviour
{
public TMP_Text npc;
public Transform options_parent;
public Button options_button;
public Button next;
public Button skip;

    private int vikramPoints = 0;
    private int chryseisPoints = 0;
    private int akiraPoints = 0;
    public int heartPoints = 0;

    public UnityEngine.UI.Image notes;
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
public UnityEngine.UI.Image character_image;

public UnityEngine.UI.Image background_panel;
public List<Sprite> background_sprites;
public List<string> background_names;
public TooManyTimers dialogue_timer; //different script
public AudioSource audioSource;
public AudioClip nextButtonSound;

void CreateFinalChoiceButtons()
{
    // Clear old buttons
    foreach (Transform child in options_parent)
    {
        Destroy(child.gameObject);
    }

    // Just one generic choice now
    Button newButton = Instantiate(options_button, options_parent);
    newButton.GetComponentInChildren<TMP_Text>().text = "Choose Ending";
    newButton.onClick.AddListener(() =>
    {
        DisableAllButtons();
        Ending();
    });

    // Add "None" button
    Button noneButton = Instantiate(options_button, options_parent);
    noneButton.GetComponentInChildren<TMP_Text>().text = "Choose None";
    noneButton.onClick.AddListener(() =>
    {
        DisableAllButtons();
        SceneManager.LoadScene("BadEnding");
    });

    options_parent.gameObject.SetActive(true);
    next.gameObject.SetActive(false);
}

void DisableAllButtons()
{
    foreach (Transform child in options_parent)
    {
        Button btn = child.GetComponent<Button>();
        if (btn != null)
        {
            btn.interactable = false;
        }
    }
}

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
        dialogue_timer.gameObject.SetActive(true);
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
        dialogue_timer.gameObject.SetActive(false);

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
    else
    {
        CreateFinalChoiceButtons();
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
        heartPoints++;
        correct_answer.Add(option.option_);
        update_journal();
            Sprite response_image = option.journal_image;
            if(response_image != null)
            {
                notes.sprite = response_image;
                notes.gameObject.SetActive(true);
            }
            
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

void Ending()
{
    if (heartPoints >= 5) // arbitrary threshold
    {
        SceneManager.LoadScene("GoodEnding");
    }
    else if (heartPoints >= 2)
    {
        SceneManager.LoadScene("MiddleEnding");
    }
    else
    {
        SceneManager.LoadScene("BadEnding");
    }
}

public void open_journal()
{
    journal_panel.SetActive(!journal_panel.activeSelf);
    update_journal();
}

public void update_journal()
{

        heartpoints_text.text =
            "Total Heart Points: " + heartPoints + "\n" +
            "Vikram: " + vikramPoints + "\n" +
            "Chryseis: " + chryseisPoints + "\n" +
            "Akira: " + akiraPoints;

        history_log.text = "Correct Answers:\n";
        foreach (string answer in correct_answer)
        {
            history_log.text += answer + "\n";
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