using UnityEngine;

public class Pop_upPanel : MonoBehaviour
{
    public GameObject panel1;
    public GameObject panel2;

    private bool panel1Shown = false;
    private bool panel2Shown = false;

    public TooManyTimers dialogue_timer;

    // Call this when rewind is clicked for the first time
    public void ShowFirstPanel()
    {
        if (!panel1Shown)
        {
            panel1.SetActive(true);
            panel1Shown = true;
            dialogue_timer.Pause_Timer();
        }
    }

    public void CloseFirstPanel()
    {
        panel1.SetActive(false);

        // Immediately show second panel after closing the first
        if (!panel2Shown)
        {
            panel2.SetActive(true);
            panel2Shown = true;
        }
    }

    public void CloseSecondPanel()
    {
        panel2.SetActive(false);
        // After this, both panels are done and won’t show again
        dialogue_timer.Resume_Timer();
    }
}
