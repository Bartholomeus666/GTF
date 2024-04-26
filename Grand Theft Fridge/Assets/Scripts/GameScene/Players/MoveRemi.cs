using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveRemi : MonoBehaviour
{

    private Vector3 _moveVector;
    [SerializeField]
    private float Speed;

    private CharacterController _characterController;

    private float _yValue;

    [SerializeField] private float JumpForce;
    [SerializeField] private float Gravity;
    

    public Animator Animator;

    private CustomInput _inputAction;

    public bool KnockedOut;
    private float _knockedOuttimer;
    [SerializeField] private float knockedOutCooldown;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputAction = new CustomInput();
        KnockedOut = false;
    }

    private void OnEnable()
    {
        _inputAction.Enable();    
    }


    private void OnDisable()
    {
        _inputAction.Disable();
    }


    public void MovePlayer(InputAction.CallbackContext context)
    {
        if (!KnockedOut)
        {
            _moveVector.x = context.ReadValue<Vector2>().x * Speed;
            _moveVector.z = context.ReadValue<Vector2>().y * Speed;
        }

    }


    private void Update()
    {
        if(KnockedOut && _knockedOuttimer < knockedOutCooldown)
        {
            _knockedOuttimer += Time.deltaTime;
        }
        else if(KnockedOut && _knockedOuttimer > knockedOutCooldown)
        {
            _knockedOuttimer = 0;
            KnockedOut = false;
        }

        if(IsMoving())
        {
            Quaternion targetRotation = Quaternion.LookRotation(-_moveVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
        }

        Animator.SetBool("isRunning", IsMoving());
    }

    private bool IsMoving()
    {
        Gamepad gamepad = Gamepad.current;

        if (gamepad != null && gamepad.leftStick.ReadValue().magnitude < 0.1f)
        {
            return false;
        }
        else { return true; }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (_characterController.isGrounded)
        {
            _yValue = 0;
            Debug.Log("Jumped");
            _yValue += JumpForce;
        }
    }
    private void FixedUpdate()
    {
        _yValue -= Gravity * Time.deltaTime;

        _characterController.Move(new Vector3(_moveVector.x, _yValue, _moveVector.z) * Time.deltaTime);
    }
}
