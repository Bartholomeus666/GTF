using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawnPoints : MonoBehaviour
{
    [SerializeField] private GameObject[] _spawnPoints = new GameObject[5];

    public GameObject GetFoodSpawn()
    {
        int index = Random.Range(0, _spawnPoints.Length);

        return _spawnPoints[index];
    }
}
