
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using Update = UnityEngine.PlayerLoop.Update;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float _newSpeedValue = 100f;
    [SerializeField] private float _boostTime = 2;
    private float _initialSpeedValue;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Getting speed value from MoveRemi script
            MoveRemi moveRemi = other.GetComponent<MoveRemi>();
            if (moveRemi != null)
            {
                // Storing the initial speed value
                _initialSpeedValue = moveRemi.Speed;
                Debug.Log(moveRemi.Speed);
                // Changing speed to new value
                moveRemi.Speed = _newSpeedValue;
                Debug.Log(moveRemi.Speed);
                // Start coroutine to revert speed back to initial value after x amount of seconds
                StartCoroutine(RevertSpeed(moveRemi));
                // Destroying the item
                gameObject.GetComponent<MeshRenderer>().enabled
                    = false;
              
            }
       
        }
    }

     IEnumerator RevertSpeed(MoveRemi moveRemi)
    {
        yield return new WaitForSeconds(_boostTime);
        moveRemi.Speed = _initialSpeedValue;
        Debug.Log(moveRemi.Speed);
        Destroy(gameObject);

        // Going back to initial speed from before item


    }
}