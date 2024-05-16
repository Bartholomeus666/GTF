using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnFood2 : MonoBehaviour
{
    public FoodSpawnPoints _fdb;

    [SerializeField] private float _minDistance;

    public GameObject Food;

    public void Respawn()
    {
        GameObject spawnpoint = _fdb.GetFoodSpawn();

        if (DistanceCheck(spawnpoint))
        {
            Instantiate(Food).gameObject.transform.position = spawnpoint.transform.position;
        }
    }
    private bool DistanceCheck(GameObject spawnpoint)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        bool enoughDistance = true;

        foreach (GameObject player in players)
        {
            float dis = Vector3.Distance(player.transform.position, spawnpoint.transform.position);

            if (dis < _minDistance)
            {
                enoughDistance = false;
            }
        }

        GameObject[] foods = GameObject.FindGameObjectsWithTag("Interactable");

        foreach (GameObject food in foods)
        {
            float dis = Vector3.Distance(food.transform.position, spawnpoint.transform.position);

            if (dis < _minDistance)
            {
                enoughDistance = false;
            }
        }
        return enoughDistance;
    }

    private void Update()
    {
        GameObject[] foods = GameObject.FindGameObjectsWithTag("Interactable");

        if (foods.Length < 4)
        {
            Respawn();
        }
    }
}
