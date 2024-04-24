using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountPlayers : MonoBehaviour
{
    private int _playerCounter;

    private void Start()
    {
        _playerCounter = 0;
    }

    public void NewPlayerSpawned()
    {
        _playerCounter++;
    }
}
