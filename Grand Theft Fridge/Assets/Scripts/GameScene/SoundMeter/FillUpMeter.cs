using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class FillUpMeter : MonoBehaviour
{
    [SerializeField] private int maxSound;
    [SerializeField] private int currentSound;
    public Image Mask;

    public bool MeterFilled = false;


    private void Awake()
    {
        MeterFilled = false;
    }

    private void Update()
    {
        if(currentSound >= maxSound)
        {
            MeterFilled = true;
        }

        GetCurrentFill();
    }

    private void GetCurrentFill()
    {
        float fillAnount = (float)currentSound / (float)maxSound;
        Mask.fillAmount = fillAnount;
    }

    public void AddSound(int amountOfSound)
    {
        currentSound += amountOfSound;
    }
}
