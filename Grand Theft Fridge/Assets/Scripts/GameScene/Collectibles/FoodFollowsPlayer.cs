using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodFollowsPlayer : MonoBehaviour
{
    public Transform Player;

    public float yOffset;

    private void Update()
    {
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + yOffset);
    }
}
