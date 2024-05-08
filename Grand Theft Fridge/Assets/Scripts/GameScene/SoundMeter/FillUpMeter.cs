using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FillUpMeter : MonoBehaviour
{
    [SerializeField] private int maxSound;
    public int currentSound;
    public Image Mask;

    public bool MeterFilled = false;

    [SerializeField] private UnityEvent SoundMeterFilled;

    private void Awake()
    {
        MeterFilled = false;
    }

    private void Update()
    {
        if(currentSound >= maxSound)
        {
            MeterFilled = true;
            SoundMeterFilled.Invoke();
        }

        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float fillAmount = (float)currentSound / (float)maxSound;
        Mask.fillAmount = fillAmount;
    }

    public void AddSound(int amountOfSound)
    {
        currentSound += amountOfSound;
    }
}
