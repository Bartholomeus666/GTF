using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimerForHuman : MonoBehaviour
{
    [SerializeField] private TMP_Text TimerText;

    public UnityEvent LookingForRats;

    private int timerID = 0;
    private float timerTime = 0;
    private float timerMax = 5;

    public bool timerRunning = false;
    public bool timerMaxedOut = false;

    private void Awake()
    {
        TimerText.text = string.Empty;
        timerRunning = false;

        Debug.Log("Timer is 0");
    }

    private void Update()
    {
        if (timerRunning && !timerMaxedOut)
        {
            if (timerTime >= timerMax)
            {
                Debug.Log("You better be hidden little rats!!");

                TimerText.text = string.Empty;

                timerMaxedOut = true;

                LookingForRats.Invoke();
            }
            else
            {
                timerTime += Time.deltaTime;

                timerID = (int)timerTime;

                TimerText.text = $"{timerID}";  
            }
        }
        else { timerTime = 0; } 
    }

    public void StartTimer()
    {
        if (!timerRunning)
        {
            timerRunning = true;
            timerTime = 0;
            Debug.Log("Timer started");
        }
        if(timerRunning && !timerMaxedOut)
        {        
            Debug.Log("Timer is already on");
        }

    }
}