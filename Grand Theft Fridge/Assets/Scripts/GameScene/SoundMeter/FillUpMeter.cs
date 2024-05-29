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
        Mask.transform.eulerAngles = new Vector3 (0f, 0f, 90 - (180* currentSound)/100);
    }

    public void AddSound(int amountOfSound)
    {
        currentSound += amountOfSound;
    }
}
