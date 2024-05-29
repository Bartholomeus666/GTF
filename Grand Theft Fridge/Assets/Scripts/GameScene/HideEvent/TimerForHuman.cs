using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class TimerForHuman : MonoBehaviour
{
    [SerializeField] private TMP_Text TimerText;

    public UnityEvent LookingForRats;

    public AudioSource src;
    public AudioClip Alarm;
    public AudioSource bgm;

    private int timerID = 0;
    private float timerTime = 0;
    private float timerMax = 5;

    public bool timerRunning = false;
    public bool timerMaxedOut = false;

    public GameObject Light;

    public GameObject UIScoreAndMeter;
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
                timerRunning = false;

                UIScoreAndMeter.SetActive(true);

                Light.SetActive(false);

                // Unmute bgm
                EnableAudioSource();

                LookingForRats.Invoke();
            }
            else
            {
                timerTime += Time.deltaTime;

                timerID = (int)timerTime;

                TimerText.text = $"HIDE BEHIND THE FOOD!\n{5 - timerID}";  
            }
        }
        else { timerTime = 0; } 
    }

    public void StartTimer()
    {
        Light.SetActive(true);

        if (!timerRunning)
        {
            UIScoreAndMeter.SetActive(false);

            timerMaxedOut = false;
            timerRunning = true;
            timerTime = 0;

            //Play Alarm and mute bgm
            src.clip = Alarm;
            src.Play();
            DisableAudioSource();

            Debug.Log("Timer started");
        }
        if(timerRunning && !timerMaxedOut)
        {        
            Debug.Log("Timer is already on");
        }

    }

    public void EnableAudioSource()
    {
        AudioSource audioSource = bgm.GetComponent<AudioSource>();

        if (bgm != null)
        {
            bgm.mute = false;

        }
        else
        {
            return;
        }
    }
    public void DisableAudioSource()
    {
        AudioSource audioSource = bgm.GetComponent<AudioSource>();

        if (bgm != null)
        {
            bgm.mute = true;

        }
        else
        {
            return;
        }
    }

}
