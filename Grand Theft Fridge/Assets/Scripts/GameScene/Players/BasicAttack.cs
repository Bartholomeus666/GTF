using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{

    private bool _attackPossible;
    private GameObject OtherPlayer;
    private MoveRemi _movementScript;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            OtherPlayer = other.gameObject;
            _attackPossible = true;
            _movementScript = OtherPlayer.GetComponent<MoveRemi>();
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
            //Destroy(OtherPlayer.gameObject);
            //OtherPlayer.gameObject.transform.position = Vector3.zero;
            _movementScript.KnockedOut = true;
        }
    }
}
