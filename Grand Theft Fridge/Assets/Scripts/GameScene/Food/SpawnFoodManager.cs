using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFoodManager : MonoBehaviour
{
    public static SpawnFoodManager SpawnFoodScript;

    [SerializeField] private Transform[] spawnPoints = new Transform[9];

    [SerializeField] private float minDistance;
    
    private Transform _currentSpawnPoint;
    
    public Transform GetSpawnPoint()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int j = 0; j < spawnPoints.Length; j++)
        {
            for (int i = 0; i < players.Length; i++)
            {
                float dis = Vector3.Distance(spawnPoints[j].position, players[i].transform.position);

                if (dis > minDistance)
                {
                    _currentSpawnPoint = spawnPoints[j];
                }
                else if (dis < minDistance)
                {
                    _currentSpawnPoint = null;
                }
            }
            if (_currentSpawnPoint != null)
            {
                return _currentSpawnPoint;
            }
        }
        return null;
    }

    //private void Update()
    //{
    //    if
    //}
}
