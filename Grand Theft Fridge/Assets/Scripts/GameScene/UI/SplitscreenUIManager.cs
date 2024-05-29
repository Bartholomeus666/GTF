using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SplitscreenUIManager : MonoBehaviour
{
    private int _counter = 0;
    public GameObject VerticalLine;
    public GameObject HorizontalLine;
    public GameObject NoOne;

    [SerializeField] private GameObject[] playerPanels = new GameObject[4];

    public void DrawingCross()
    {
        _counter++;

        if(_counter == 2)
        {
            VerticalLine.SetActive(true);
            NoOne.SetActive(false);
        }
        else if (_counter == 3)
        {
            HorizontalLine.SetActive(true);
        }

        playerPanels[_counter - 1].SetActive(true);
    }
}
