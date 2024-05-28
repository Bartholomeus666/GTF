using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MoveRemi : MonoBehaviour
{

    public Vector3 MoveVector;
    [SerializeField]
    public float Speed;

    public CharacterController CharacterController;

    public float yValue;

    public float JumpForce;
    [SerializeField] private float Gravity;

    [SerializeField] private float Rotation;


    public bool KnockedOut;
    private float _knockedOuttimer;
    [SerializeField] private float knockedOutCooldown;

    private bool _IsCaught = false;

    private SpawnPointData spawnPointData;

    public bool Respawning;

    private AnimationController _animationController;


    public UnityEvent AnimateRun;
    public UnityEvent AnimateJump;
    public UnityEvent AnimateFlying;
    public UnityEvent AnimateDead;




    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        _animationController = GetComponentInChildren<AnimationController>();
        
        KnockedOut = false;
        _IsCaught = false;
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        if (!KnockedOut)
        {
            MoveVector.x = context.ReadValue<Vector2>().x * Speed;
            MoveVector.z = context.ReadValue<Vector2>().y * Speed;
        }
    }


    private void Update()
    {
        if (!_IsCaught)
        {
            if (CharacterController.isGrounded && yValue < 0)
            {
                _animationController.BackToIdle();
                yValue = 0;
            }

            if (!CharacterController.isGrounded)
            {
                AnimateFlying.Invoke();
            }

            if (KnockedOut && _knockedOuttimer < knockedOutCooldown)
            {
                _knockedOuttimer += Time.deltaTime;
            }
            else if (KnockedOut && _knockedOuttimer > knockedOutCooldown)
            {
                _knockedOuttimer = 0;
                KnockedOut = false;

                MoveVector = Vector3.zero;
            }

            if (GettingInput() && IsMoving())
            {
                Quaternion targetRotation = Quaternion.LookRotation(MoveVector);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime * Rotation);
                AnimateRun.Invoke();
            }
            else
            { 
                if(CharacterController.isGrounded)
                {
                    _animationController.BackToIdle();
                }
            }

            yValue -= Gravity * Time.deltaTime;
            if (!_IsCaught)
            {
                CharacterController.Move(new Vector3(MoveVector.x, yValue, MoveVector.z) * Time.deltaTime);
            }
        }
        else
        {
            AnimateDead.Invoke();
        }
    }

    public bool IsMoving()
    {
        if (MoveVector.x != 0 || MoveVector.z != 0)
        {
            return true;
        }
        else { return false; }
    }

    private bool GettingInput()
    {
        Gamepad gamepad = Gamepad.current;

        if (gamepad != null && gamepad.leftStick.ReadValue().magnitude < 0.1f)
        {
            return false;
        }
        else { return true; }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (CharacterController.isGrounded)
        {
            AnimateJump.Invoke();
            yValue = 0;
            Debug.Log("Jumped");
            yValue += JumpForce;
        }
    }
    private void FixedUpdate()
    {
        if (Respawning)
        {
            Respawn();
            Respawning = false;
        }
    }

    public void RemiGotCaught()
    {
        _IsCaught = true;

        Debug.Log("you got caught, Remiiiii");
    }

    public void Respawn()
    {
        GameObject respawnCollection = GameObject.FindGameObjectWithTag("Respawn");

        spawnPointData = respawnCollection.GetComponent<SpawnPointData>();
        SpawnAndAssign spawnScript = GetComponent<SpawnAndAssign>();

        transform.position = spawnPointData.GetSpawnPoints(spawnScript.PlayerID).transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, spawnPointData.GetSpawnPoints(spawnScript.PlayerID - 1).transform.position, 1);
    }
}
