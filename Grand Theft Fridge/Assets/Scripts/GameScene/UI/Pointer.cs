using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour
{
    public bool IsAssigned;

    public GameObject Player;

    private void Start()
    {
        IsAssigned = false;
    }

    private void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1.55f, Player.transform.position.z);
    }
}
