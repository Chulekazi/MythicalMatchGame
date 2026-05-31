using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


    [System.Serializable]
public class DialogueLine
{
    public string characterName;
    [TextArea(2, 5)]
    public string dialogueText;
}

public class ManagerScript : MonoBehaviour
{
    [Header("Dialogue Sequence")]
    public DialogueLine[] dialogueLines;
    private int currentLineIndex = 0;

    [Header("UI References")]
    public TMP_Text nameTextUI;
    public TMP_Text dialogueTextUI;
    public Button nextButton;

    [Header("Typewriter Settings")]
    public float typingSpeed = 0.03f;

    [Header("Audio Settings")]
    public AudioSource audioSource;   
    public AudioClip nextButtonSound; 

    public string mainMenuSceneName = "menu";

    void Start()
    {
        DisplayCurrentLine();
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void DisplayCurrentLine()
    {
        if (dialogueLines.Length == 0) return;

        DialogueLine line = dialogueLines[currentLineIndex];

        if (nameTextUI != null)
            nameTextUI.text = line.characterName;

        if (dialogueTextUI != null)
            StartCoroutine(TypeText(line.dialogueText));
    }

    IEnumerator TypeText(string textToType)
    {
        nextButton.interactable = false;
        dialogueTextUI.text = "";

        foreach (char c in textToType)
        {
            dialogueTextUI.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        nextButton.interactable = true;
    }

    void OnNextButtonClicked()
    {
        
        if (audioSource != null && nextButtonSound != null)
            audioSource.PlayOneShot(nextButtonSound);

        NextLine();
    }

    void NextLine()
    {
        if (currentLineIndex < dialogueLines.Length - 1)
        {
            currentLineIndex++;
            DisplayCurrentLine();
        }
        else
        {
            Debug.Log("Dialogue finished!");
            FindFirstObjectByType<SceneTransition>().FadeToMainMenu();
        }
    }
}