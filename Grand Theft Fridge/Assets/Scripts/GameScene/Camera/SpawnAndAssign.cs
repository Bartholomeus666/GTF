using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndAssign : MonoBehaviour
{
    public Camera PlayerCam;

    public void SpawnCamera()
    {
        Instantiate(PlayerCam);
    }
}
