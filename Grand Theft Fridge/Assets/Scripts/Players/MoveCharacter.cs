using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveCharacter : MonoBehaviour
{
    private CustomInput inputActions;
    private CharacterController _characterController;


    [SerializeField] private float Speed = 5;

    private void Awake()
    {
        inputActions = new CustomInput();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Movement.performed += MovePlayer;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void FixedUpdate()
    {
        
    }

    private void MovePlayer(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());

        Vector2 direction = context.ReadValue<Vector2>();

        _characterController.Move(new Vector3(direction.x, 0, direction.y) * Speed * Time.deltaTime);
    }
}
