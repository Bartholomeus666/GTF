using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainCamera : MonoBehaviour
{
    public Camera Camera;
    public GameObject BackGround;

    public GameObject UISplitscreen;


    private GameObject[] _players = new GameObject[4];
    public void ZoomOut()
    {
        Camera.depth = 1;
        BackGround.SetActive(false);

        UISplitscreen.SetActive(false);
    }

    public void ZoomIn()
    {
        Camera.depth = -1;
        BackGround.SetActive(true);

        UISplitscreen.SetActive(true);

        _players = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject p in _players)
        {
            if (p != null)
            {
                MoveRemi moveScript = p.GetComponent<MoveRemi>();
                
                moveScript.enabled = true;
            }
        }
    }
}
