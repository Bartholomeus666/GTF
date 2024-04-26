using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    private bool _attackPossible;
    private GameObject OtherPlayer;
    private MoveRemi _movementScript;

    private bool _grabPossible;
    private Food _foodScript;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            OtherPlayer = other.gameObject;
            _attackPossible = true;
            _movementScript = OtherPlayer.GetComponent<MoveRemi>();
        }
        else if(other.tag == "Interactable")
        {
            Debug.Log("found food!");

            OtherPlayer = other.gameObject;
            _grabPossible = true;
            _foodScript = OtherPlayer.GetComponent<Food>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _attackPossible = false;
        _grabPossible = false;
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
            _movementScript.KnockedOut = true;
        }
    }

    public void GrabPerformed()
    {
        if(_grabPossible)
        {
            _foodScript.Grabbed = true;
            _foodScript.Player = this.gameObject.transform;
        }
    }
}
