using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainCamera : MonoBehaviour
{
    public Camera Camera;
    public GameObject BackGround;

    public GameObject UISplitscreen;


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
    }
}
