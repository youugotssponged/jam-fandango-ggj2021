using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject TimerUI_Canvas;
    private Timer timer;
    public static double time; 
    private static GameManager instance = null;
    public static GameManager Instance 
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject); // destroy the object this is attached to.
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject); // Persist the object that this is attached to across scene loading
    }

    void Start()
    {
        timer = TimerUI_Canvas.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        time = timer.timerTime;
    }

    public void KillInstance() {
        Destroy(gameObject);
    }
    public void PrintTime(){
        Debug.Log("Time is: " + time);
    }

    public double getTime() {
        return time;
    }

}
