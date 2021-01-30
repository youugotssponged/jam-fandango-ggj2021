using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private float stopTime;
    public float timerTime;
    public int mins;
    public int secs;
    private bool isRunning;

    public bool debug_timer = false;
    public bool shouldStart = false;

    void Start()
    {
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldStart)
        {
            timerTime = stopTime + (Time.time - startTime);

            mins = (int)timerTime / 60;
            secs = (int)timerTime % 60;

            if (isRunning && debug_timer)
            {
                Debug.Log($"{mins}:{secs}");
            }

            timerText.text = $"{mins} minutes : {secs} seconds";
        }
    }

    public void TimerStart()
    {
        if (!isRunning)
        {
            isRunning = true;
            shouldStart = true;
            startTime = Time.time;
        }
    }

    public void Stop()
    {
        if (isRunning)
        {
            isRunning = false;
            shouldStart = false;
            stopTime = timerTime;
        }
    }

    public void Reset()
    {
        stopTime = 0;
        isRunning = false;
    }

}
