using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private string _currentAnimation = string.Empty;

    private MoveRemi _moveScript;
    private BasicAttack _basicAttackScript;

    public enum RatState
    {
        Idle,
        Running,
        Attack,
        Dead,
        Jump,
        GettingAttacked,
        Falling
    }

    private RatState _state;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _moveScript = GetComponentInParent<MoveRemi>();
        _basicAttackScript = GetComponentInParent<BasicAttack>();
    }

    private void Update()
    {
        CheckBasicAnimation();
    }

    private void ChangeAnimation(string animationName, float crossfade = 0.5f)
    {
        if(_currentAnimation != animationName)
        {
            _currentAnimation = animationName;
            _animator.CrossFade(animationName, crossfade);
        }
    }

    private void CheckBasicAnimation()
    {
        if (_moveScript.CharacterController.isGrounded && _basicAttackScript.ReturBoolForAttack())
        {
            _state = RatState.Attack;
        }
        else if (_moveScript.IsMoving())
        {
            _state = RatState.Running;
        }
        else if (!_moveScript.IsMoving())
        {
            _state= RatState.Idle;
        }
        

        ChangeAnimation($"{_state}");
    }
}
