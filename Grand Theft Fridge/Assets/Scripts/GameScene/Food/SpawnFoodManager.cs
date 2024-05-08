using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFoodManager : MonoBehaviour
{
    public static SpawnFoodManager SpawnFoodScript;

    [SerializeField] private Transform[] spawnPoints = new Transform[9];

    [SerializeField] private float minDistance;
    
    private Transform _currentSpawnPoint;

    [SerializeField] private GameObject[] foods = new GameObject[3];
    private GameObject _foodToBeSpawned;


    private void Start()
    {
        foods = GameObject.FindGameObjectsWithTag("Interactable");

        foods[0].transform.position = spawnPoints[5].transform.position;
        foods[1].transform.position = spawnPoints[6].transform.position;
        foods[2].transform.position = spawnPoints[3].transform.position;
    }

    public Transform GetSpawnPoint()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Interactable");

        for(int j = 0; j < spawnPoints.Length; j++)
        {
            for (int i = 0; i < players.Length; i++)
            {
                float dis = Vector3.Distance(spawnPoints[j].position, players[i].transform.position);

                if (dis > minDistance)
                {   
                    for(int k = 0; k < foods.Length; k++)
                    {
                        float disToFood = Vector3.Distance(spawnPoints[j].position, foods[k].transform.position);

                        if(disToFood > minDistance)
                        {
                            _currentSpawnPoint = spawnPoints[j];
                        }
                        else if (disToFood < minDistance)
                        {
                            _currentSpawnPoint = null;
                        }
                    }
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
            else { _currentSpawnPoint= spawnPoints[7]; }
        }
        return null;
    }

    private void Update()
    {
        for (int j = 0;j < foods.Length; j++)
        {
            if(!foods[j].activeSelf)
            {
                _foodToBeSpawned = foods[j];
                SpawnInactiveFood();
            }
        }
    }

    private void SpawnInactiveFood()
    {
        _foodToBeSpawned.transform.position = GetSpawnPoint().position;
        _foodToBeSpawned.SetActive(true);
    }
}
