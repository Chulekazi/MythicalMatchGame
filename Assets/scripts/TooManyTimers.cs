using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TooManyTimers : MonoBehaviour
{
    public Image timer_;
    public float limit = 10f;
    private float remaining_time;
    private bool isPaused = false;

    public delegate void timer_finished();
    public event timer_finished OnTimerFinished;

    public void Start_Timer()
    {
        remaining_time = limit;
        StartCoroutine(RunTimer());
        isPaused = false;
    }

    public void Stop_Timer()
    {
        StopAllCoroutines();
        timer_.fillAmount = 1f; // reset to full circle
    }

    public void Pause_Timer()
    {
        isPaused = true;
    }

    public void Resume_Timer()
    {
        isPaused = false;
    }

    IEnumerator RunTimer()
    {
        while (remaining_time > 0)
        {
            if(isPaused)
            {
                yield return null;
                continue;
            }

            remaining_time -= Time.deltaTime;
            timer_.fillAmount = remaining_time / limit;
            yield return null;
        }

        // Timer finished
        timer_.fillAmount = 0;
        OnTimerFinished?.Invoke();
    }
}