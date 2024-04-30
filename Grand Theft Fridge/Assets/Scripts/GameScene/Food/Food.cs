using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool Grabbed = false;

    public Transform Player;

    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    private void Update()
    {
        if (Grabbed)
        {
            transform.position = new Vector3(Player.position.x + Player.forward.x, Player.position.y + yOffset, Player.position.z + Player.forward.z + zOffset);
        }
    }
}
