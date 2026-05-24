using UnityEngine;
using UnityEngine.UI;

public class Rewind_button : MonoBehaviour
{
    public Button rewind;
    private BIGMANAGER manager;
    private dialogue_optionSO lastSelectedOption;
    private bool rewind_used = false;
    public GameObject panel;
    public AudioSource audioSource;      
    public AudioClip rewindSound;

    void Start()
    {
        manager = Object.FindFirstObjectByType<BIGMANAGER>();

        rewind.onClick.RemoveAllListeners();
        rewind.onClick.AddListener(HandleRewind);
        rewind.gameObject.SetActive(false);
    }

    private void HandleRewind()
    {
        if (lastSelectedOption != null && !rewind_used)
        {
            rewind_used = true;
            rewind.interactable = false;

            if (audioSource != null && rewindSound != null)
            {
                audioSource.PlayOneShot(rewindSound);
            }

            // Show the first tutorial panel
            Object.FindFirstObjectByType<Pop_upPanel>()?.ShowFirstPanel();

            manager.Rewind(lastSelectedOption);
        }
    }
    public void EnableRewind(dialogue_optionSO option)
    {
        lastSelectedOption = option;
        if (!rewind_used)
        {
            rewind.gameObject.SetActive(true);
        }
    }
    public void Reset_Rewind()
    {
        rewind_used = false;
        lastSelectedOption = null;
        rewind.interactable = true;   // re‑enable click
        rewind.gameObject.SetActive(false); // keep hidden until needed
    }

    public void Register(dialogue_optionSO option)
    {
        lastSelectedOption = option;

    }

}
