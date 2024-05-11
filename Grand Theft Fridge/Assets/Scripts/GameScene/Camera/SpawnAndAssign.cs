using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAndAssign : MonoBehaviour
{
    public Camera PlayerCam;

    public int PlayerID;

    private SpawnPointData spawnPointData;

    private bool AlreadySpawned = false;

    private void Awake()
    {
        if(AlreadySpawned)
            return;


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject respawnCollection = GameObject.FindGameObjectWithTag("Respawn");

        spawnPointData = respawnCollection.GetComponent<SpawnPointData>();

        PlayerID = players.Length;

        AlreadySpawned = true;
    }


    public void SpawnCamera()
    {
        Instantiate(PlayerCam);
    }

    public void Respawn()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPointData.GetSpawnPoints(PlayerID -1).transform.position, 500);
    }
}
