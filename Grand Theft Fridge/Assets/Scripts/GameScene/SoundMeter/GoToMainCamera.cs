using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainCamera : MonoBehaviour
{
    public Camera Camera;
    public GameObject BackGround;


    private void Start()
    {
        Camera = GetComponent<Camera>();
    }

    public void ZoomOut()
    {
        Camera.depth = 1;
        BackGround.SetActive(false);
    }
}
