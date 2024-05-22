using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFoodPointers : MonoBehaviour
{
    [SerializeField] private GameObject foodPointer;
    public static int Counter = 0;

    private void Start()
    {
        Counter = 0;
    }

    public int GetCounter()
    {
        return Counter;
    }

    public void SpawnFoodPointer()
    {
        Instantiate(foodPointer);
        Counter++;
    }
}
