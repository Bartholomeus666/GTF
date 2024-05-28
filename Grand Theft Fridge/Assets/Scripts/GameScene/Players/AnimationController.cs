using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }


    public void BackToIdle()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsGrounded", true);
        _animator.SetBool("IsJumping", false);
    }

    public void RunningAnimation()
    {
        _animator.SetBool("IsRunning", true);
    }

    public void JumpingAnimation()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsJumping", true );
    }

    public void FlyingAnimation()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsGrounded", false);
    }
    public void AttackAnimation()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsAttacking", true);
    }

    public void DeathAnimation()
    {
        _animator.SetBool("IsRunning", false);
        _animator.SetBool("IsGrounded", true);
        _animator.SetBool("IsJumping", false);
        _animator.SetBool("Died", true);
    }
}
