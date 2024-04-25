using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    private bool _attackPossible;
    private GameObject OtherPlayer;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            OtherPlayer = other.gameObject;
            _attackPossible = true;
        }
    }

    private void Update()
    {
        _attackPossible = false;
        OtherPlayer = null;
    }
    public void BasicAttackPerformed()
    {
        if (_attackPossible)
        {
            OtherPlayer.transform.position = Vector3.zero;
        }
    }
}
