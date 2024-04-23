using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveRemi : MonoBehaviour
{

    private Vector3 _moveVector;
    [SerializeField]
    private float Speed;

    private CharacterController _characterController;

    private float _yValue;

    private bool _hasDoubleJumped;
    private float _doubleJumpTimer;
    [SerializeField] private float CooldownTime;

    [SerializeField] private float JumpForce;
    [SerializeField] private float Gravity;

    private CustomInput _inputAction;


    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputAction = new CustomInput();
    }

    private void OnEnable()
    {
        _inputAction.Enable();
        _inputAction.Player.Movement.performed += MovePlayer;
    }


    private void OnDisable()
    {
        _inputAction.Disable();
        _inputAction.Player.Movement.performed -= MovePlayer;
    }


    private void MovePlayer(InputAction.CallbackContext context)
    {
        _moveVector.x = context.ReadValue<Vector2>().x * Speed;
        _moveVector.z = context.ReadValue<Vector2>().y * Speed;
    }


    private void Update()
    {
        if (_characterController.isGrounded) 
        {
            _yValue = 0f;
            _hasDoubleJumped = false;
            _doubleJumpTimer = 0f;
        }
        if (!IsMoving())
        {
            _moveVector = Vector3.zero;
        }
    }

    private bool IsMoving()
    {
        Keyboard keyboard = Keyboard.current;
        Gamepad gamepad = Gamepad.current;

        if (keyboard != null && !keyboard.anyKey.isPressed)
        {
            return false;
        }

        if (gamepad != null && gamepad.leftStick.ReadValue().magnitude < 0.1f)
        {
            return false;
        }
        else { return true; }
    }

    private void Jump()
    {
        _yValue += JumpForce;
    }

    private void FixedUpdate()
    {
        _yValue -= Gravity;

        _characterController.Move(new Vector3(_moveVector.x, _yValue, _moveVector.z) * Time.deltaTime);
    }

}
