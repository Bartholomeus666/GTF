using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundMeterEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent MoveCamera;
    [SerializeField] private UnityEvent StartTimer;

    [SerializeField] private FillUpMeter MeterBoolScript;
    private bool _eventsInvoked = false;


    public void MeterFilled()
    {
        MoveCamera.Invoke();
        StartTimer.Invoke();

        //_eventsInvoked = true;
        MeterBoolScript.currentSound = 0;
        MeterBoolScript.MeterFilled = false;
    }
}
