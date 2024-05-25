using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private string _currentAnimation = "Idle";

    private MoveRemi _moveScript;
    private BasicAttack _basicAttackScript;

    //public enum RatState
    //{
    //    Idle,
    //    Running,
    //    Attack,
    //    Dead,
    //    Jump,
    //    GettingAttacked,
    //    Falling
    //}

    //public RatState State;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _moveScript = GetComponentInParent<MoveRemi>();
        _basicAttackScript = GetComponentInParent<BasicAttack>();
    }

    private void Update()
    {
        //BackToIdle();
        //ChangeAnimation();
    }

    public void ChangeAnimation(string animationName, float crossfade = 0.5f, float time = 0)
    {
        if (time > 0) StartCoroutine(Wait());
        else Validate();

        IEnumerator Wait()
        {
            yield return new WaitForSeconds(time /*- crossfade*/);
            Validate();
        }

        void Validate()
        {
            if (_currentAnimation != animationName)
            {
                _currentAnimation = animationName;
                if (_currentAnimation == "")
                    _currentAnimation = "Idle";
                else
                    _animator.CrossFade(animationName, crossfade);
            }
        }
    }

    public void BackToIdle()
    {
        if(_currentAnimation == "Attack")
        {
            return;
        }

        ChangeAnimation("Idle");
    }
}
