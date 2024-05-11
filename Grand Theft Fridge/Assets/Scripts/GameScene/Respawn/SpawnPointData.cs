using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointData : MonoBehaviour
{
    public static SpawnPointData Instance;
    
    public GameObject[] SpawnPlayers = new GameObject[4];

    public GameObject GetSpawnPoints(int index)
    {
        return SpawnPlayers[index];
    }

}
