using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RandomizerDatabase : ScriptableObject
{
    public Randomizer[] Skins;

    public int SkinsCount
    {
        get
        {
            return Skins.Length;
        }
    }

    public Randomizer GetRandomizer(int index)
    {
        return Skins[index];
    }
}
