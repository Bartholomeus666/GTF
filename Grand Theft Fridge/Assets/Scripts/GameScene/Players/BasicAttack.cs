using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
<<<<<<< Updated upstream
    // Start is called before the first frame update
    void Start()
    {
        
=======

    private bool _attackPossible;
    private GameObject OtherObject;
    private MoveRemi _movementScript;

    private bool _grabPossible;
    private FoodFollowsPlayer _playerFollowScript;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            OtherObject = other.gameObject;
            _attackPossible = true;
            _movementScript = OtherObject.GetComponent<MoveRemi>();
        }
        else if(other.tag == "Grabable Object")
        {
            Debug.Log("Found food!");

            OtherObject = other.gameObject;
            _grabPossible = true;
            _playerFollowScript = OtherObject.GetComponent<FoodFollowsPlayer>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _attackPossible = false;
        _grabPossible = false;
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
        
=======
        _attackPossible = false;
        OtherObject = null;
    }
    public void BasicAttackPerformed()
    {
        if (_attackPossible)
        {
            _movementScript.KnockedOut = true;
        }
>>>>>>> Stashed changes
    }
    public void GrabPerformed()
    {
        Debug.Log("Grab pushed");

        if(_grabPossible)
        {
            Debug.Log("Grab was possible");

            _playerFollowScript.Player = this.gameObject.transform;
            _playerFollowScript.yOffset = 2;
        }
    }
}
