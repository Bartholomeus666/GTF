using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RandomizerDatabase : ScriptableObject
{
    public GameObject[] Skins = new GameObject[3];

    public int SkinsCount
    {
        get
        {
            return Skins.Length;
        }
    }

    public GameObject GetRandomizer(int index)
    {
        return Skins[index];
    }
}
