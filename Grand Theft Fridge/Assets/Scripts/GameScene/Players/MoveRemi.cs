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
    private bool _falling;

    private CustomInput _inputAction;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputAction = new CustomInput();
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
        _moveVector.x = context.ReadValue<Vector2>().x * Speed;
        _moveVector.z = context.ReadValue<Vector2>().y * Speed;
    }


    private void Update()
    {
        if (_characterController.isGrounded)
        {

        }

<<<<<<< Updated upstream
        //    if (!IsMoving())
        //    {
        //        _moveVector.x = 0f;
        //        _moveVector.z = 0f;
        //    
=======

        if(_characterController.isGrounded && _yValue < 0)
        {
            _yValue = 0;
        }

        if(IsMoving())
        {
            Quaternion targetRotation = Quaternion.LookRotation(-_moveVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
        }

        Animator.SetBool("isRunning", IsMoving());
>>>>>>> Stashed changes
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
