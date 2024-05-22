using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPointers : MonoBehaviour
{
    private (int left, int right, int up, int down) border;

    private int pointerNr;

    private RectTransform _rectTransform;

    private Vector3 _targetPosition = Vector3.zero;

    private Camera _camera;
    Camera[] cameras;

    private void Start()
    {
        pointerNr = SpawnFoodPointers.Counter;

        _rectTransform = GetComponent<RectTransform>();
        cameras = FindObjectsOfType<Camera>();

        Debug.Log("huh");

        _camera = AssignCamera();

        switch (pointerNr)
        {
            case 1:
                Debug.Log("1 Player Spawned");

                border.left = 0;
                border.right = Screen.width / 2;
                border.up = Screen.height;
                border.down = 0;
                return;
            case 2:
                border.left = Screen.width / 2;
                border.right = Screen.width;
                border.up = Screen.height;
                border.down = 0;
                return;
        }
    }

    private Camera AssignCamera()
    {
        Debug.Log("Assigning");

        foreach (Camera cam in cameras)
        {
            if (cam.gameObject.tag.Equals("PlayerCamera"))
            {
                Debug.Log("Are we even checking");

                ConnectCameraToPlayer connectCameraToPlayer = cam.GetComponent<ConnectCameraToPlayer>();

                Debug.Log(connectCameraToPlayer.CamerId);
                if(connectCameraToPlayer.CamerId == pointerNr)
                {
                    Debug.Log("Assigned");

                    return cam;
                }
            }
        }
        return null;
    }

    private void Update()
    {
        Vector3 toPostion = _targetPosition;
        Vector3 fromPosition = _camera.transform.position;
        fromPosition.z = 0;

        Vector3 dir = (toPostion - fromPosition).normalized;

        float angle = Vector3.Angle(dir, toPostion);

        _rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
