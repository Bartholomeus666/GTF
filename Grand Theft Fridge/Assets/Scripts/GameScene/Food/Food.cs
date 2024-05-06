using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool Grabbed = false;

    public GameObject Player;
    private BasicAttack _grabScript;

    [SerializeField] private float yOffset;
    [SerializeField] private float zOffset;

    

    private void Update()
    {
        if (Grabbed)
        {
            //transform.position = new Vector3(Player.transform.position.x + Player.transform.forward.x, Player.transform.position.y + yOffset, Player.transform.position.z + Player.transform.forward.z + zOffset);
            transform.position = Player.transform.position + Player.transform.TransformDirection(new Vector3(0, 1.2f, 1.2f));

            //transform.parent = Player.transform;

            _grabScript = Player.GetComponentInParent<BasicAttack>();

            if (!_grabScript.IsHoldingFood)
            {
                Grabbed = false;
                //transform.parent = null;
            }
        }
        else {Player = this.gameObject;}


    }
}
