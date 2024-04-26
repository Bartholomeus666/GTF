using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool Grabbed = false;

    public Transform Player;

    [SerializeField] private float yOffset;
    private void Update()
    {
        if (Grabbed)
        {
            transform.position = new Vector3(Player.position.x, Player.position.y + yOffset, Player.position.z);
        }
    }
}
