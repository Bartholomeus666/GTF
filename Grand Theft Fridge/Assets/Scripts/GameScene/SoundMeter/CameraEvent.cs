using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent MoveCamera;

    public bool MeterIsFull;


    private void Update()
    {
        MeterFilled();
    }
    public void MeterFilled()
    {
        if (MeterIsFull)
        {
            MoveCamera.Invoke();
        }
    }
}
