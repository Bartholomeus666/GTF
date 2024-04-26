using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitscreenUIManager : MonoBehaviour
{
    private int _counter = 0;
    public GameObject VerticalLine;
    public GameObject HorizontalLine;

    public void DrawingCross()
    {
        _counter++;

        if(_counter == 2)
        {
            VerticalLine.SetActive(true);
        }
        else if (_counter == 3)
        {
            HorizontalLine.SetActive(true);
        }
    }
}
