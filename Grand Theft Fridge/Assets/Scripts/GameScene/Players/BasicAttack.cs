using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BasicAttack : MonoBehaviour
{
    private Food _foodScript;
    [SerializeField] private float PickUpRadius = 2;
    [SerializeField] private float ForwardOffset = 2;
    [SerializeField] private float PushForce = 2;


    private GameObject _soundMeter;
    private FillUpMeter _fillUpMeterScript;

    public bool IsHoldingFood = false;

    private void Start()
    {
        _soundMeter = GameObject.FindGameObjectWithTag("SoundMeter");
        _fillUpMeterScript = _soundMeter.GetComponent<FillUpMeter>();
    }


    public void BasicAttackPerformed()
    {
        Debug.Log("Attacking");

        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * ForwardOffset, PickUpRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider c = colliders[i];

            if (c.tag == "Player")
            {
                Debug.Log("Opponent attacked");

                MoveRemi moveScript=  c.gameObject.GetComponent<MoveRemi>();
                BasicAttack grabbedScript= c.gameObject.GetComponent<BasicAttack>();

                grabbedScript.IsHoldingFood = false;

                moveScript.KnockedOut = true;
                moveScript.MoveVector = transform.forward * PushForce;
                _fillUpMeterScript.AddSound(5);
            }
        }
    }

    public void GrabPerformed()
    {
        Debug.Log("Grabbing");
        if(!IsHoldingFood)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward * ForwardOffset, PickUpRadius);
        
            for (int i = 0; i < colliders.Length; i++)
            {
                Collider c = colliders[i];

                if (c.tag == "Interactable")
                {
                    Debug.Log("Food found!");

                    IsHoldingFood = true;
                    Food foodScript = c.gameObject.GetComponent<Food>();
                    if(!foodScript.Grabbed)
                    {
                        foodScript.Grabbed = true;
                        foodScript.Player = this.gameObject;
                        _fillUpMeterScript.AddSound(5);
                    }

                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position + transform.forward * ForwardOffset, PickUpRadius);
    }
}
