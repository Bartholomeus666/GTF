using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitscreenUIManager : MonoBehaviour
{
    private int _counter = 0;
    public GameObject VerticalLine;
    public GameObject HorizontalLine;

    [Header("PlayerScores")]
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;

    public void DrawingCross()
    {
        _counter++;

        if(_counter == 1)
        {
            Player1.SetActive(true);
        }
        else if(_counter == 2)
        {
            VerticalLine.SetActive(true);
            Player2.SetActive(true);
        }
        else if (_counter == 3)
        {
            HorizontalLine.SetActive(true);
            Player3.SetActive(true);
        }
        else
        {
            Player4.SetActive(true);
        }
    }
}
