using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundMeterEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent MoveCamera;
    [SerializeField] private UnityEvent StartTimer;

    [SerializeField] private FillUpMeter MeterBoolScript;


    private void Update()
    {
        MeterFilled();
    }
    public void MeterFilled()
    {
        if (MeterBoolScript.MeterFilled)
        {
            MoveCamera.Invoke();
            StartTimer.Invoke();
        }
    }
}
