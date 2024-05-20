using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnAndAssign : MonoBehaviour
{
    [SerializeField] private UnityEvent AssignColor;

    public Camera PlayerCam;

    public int PlayerID;

    private SpawnPointData spawnPointData;

    private bool AlreadySpawned = false;

    private void Awake()
    {
        if (AlreadySpawned)
            return;


        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject respawnCollection = GameObject.FindGameObjectWithTag("Respawn");

        spawnPointData = respawnCollection.GetComponent<SpawnPointData>();

        PlayerID = players.Length;

        AssignColor.Invoke();

        AlreadySpawned = true;
    }


    public void SpawnCamera()
    {
        Instantiate(PlayerCam);
    }

    public void Respawn()
    {
        MoveRemi moveRemi = gameObject.GetComponent<MoveRemi>();

        moveRemi.MoveVector = Vector3.zero;

        transform.position = Vector3.MoveTowards(transform.position, spawnPointData.GetSpawnPoints(PlayerID - 1).transform.position, 500);
    }
}
