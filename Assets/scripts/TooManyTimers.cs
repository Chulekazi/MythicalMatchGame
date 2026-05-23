using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TooManyTimers : MonoBehaviour
{
    public Image timer_;
    public float limit = 10f;
    private float remaining_time;

    public delegate void timer_finished();
    public event timer_finished OnTimerFinished;

    public void Start_Timer()
    {
        remaining_time = limit;
        StartCoroutine(RunTimer());
    }
    public void Stop_Timer()
    {
        StopAllCoroutines();
        timer_.fillAmount = 1f; // reset to full circle
    }
    IEnumerator RunTimer()
    {
        while (remaining_time > 0)
        {
            remaining_time -= Time.deltaTime;
            timer_.fillAmount = remaining_time / limit;
            yield return null;
        }

        // Timer finished
        timer_.fillAmount = 0;
        OnTimerFinished?.Invoke();
    }

}
