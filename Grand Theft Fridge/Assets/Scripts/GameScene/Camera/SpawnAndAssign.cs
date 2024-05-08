using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndAssign : MonoBehaviour
{
    public Camera PlayerCam;

    public int PlayerID;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        PlayerID = players.Length;
    }


    public void SpawnCamera()
    {
        Instantiate(PlayerCam);
    }
}
