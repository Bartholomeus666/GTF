
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float _newSpeedValue = 100f;
    [SerializeField] private float _boostTime = 2;
    private float _initialSpeedValue;
    [SerializeField] private GameObject trail;
    private GameObject _ratTrail;

    public AudioSource src;
    public AudioClip _speedSound;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            src.clip = _speedSound;
            src.Play();

            // Getting speed value from MoveRemi script
            MoveRemi moveRemi = other.GetComponent<MoveRemi>();
            if (moveRemi != null)
            {
                _ratTrail = Instantiate(trail, other.transform);
                // Storing the initial speed value
                _initialSpeedValue = moveRemi.Speed;
                Debug.Log(moveRemi.Speed);
                // Changing speed to new value
                moveRemi.Speed = _newSpeedValue;
                Debug.Log(moveRemi.Speed);
                // Start coroutine to revert speed back to initial value after x amount of seconds
                StartCoroutine(RevertSpeed(moveRemi));
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<MeshCollider>().enabled = false;
                StartCoroutine(Respawn());
            }
        }
    }

    void Update()
    {
        transform.eulerAngles = new(0f, transform.eulerAngles.y + Time.deltaTime * 50f, 0f);
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(5f);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;

    }

    IEnumerator RevertSpeed(MoveRemi moveRemi)
    {
        yield return new WaitForSeconds(_boostTime);
        moveRemi.Speed = _initialSpeedValue;
        Debug.Log(moveRemi.Speed);
        Destroy(_ratTrail);
        // Going back to initial speed from before item
    }
}