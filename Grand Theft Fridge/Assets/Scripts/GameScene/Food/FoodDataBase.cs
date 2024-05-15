using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FoodDataBase : ScriptableObject
{
    public static FoodDataBase instance;

    public GameObject[] Skins = new GameObject[4];

    public GameObject GetSpawnPoint()
    {
        int ran = Random.Range(0, Skins.Length);

        return Skins[ran];
    }
}
