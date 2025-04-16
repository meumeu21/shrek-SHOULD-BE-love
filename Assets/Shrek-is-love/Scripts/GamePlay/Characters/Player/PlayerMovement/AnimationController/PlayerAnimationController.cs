using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void PlayAttack()
    {
        _animator.SetTrigger("Attack");
    }
    
    public void PlayYell()
    {
        _animator.SetTrigger("Yell");
    }

    public void PlayJump(bool isJumping)
    {
        _animator.SetBool("isJumping", isJumping);
    }

    public void SetMagnitude(float inputMagnitude, float dampingTime, float time)
    {
        _animator.SetFloat("inputMagnitude", inputMagnitude, dampingTime, time);
    }

    public void SetGrounded(bool isGrounded)
    {
        _animator.SetBool("isGrounded", isGrounded);
    }

    public bool isGrounded()
    {
        return _animator.GetBool("isGrounded");
    }

    public bool IsAnimationPlaying(string animationName, int layerIndex = 0)
    {
        AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(layerIndex);
        bool isPlaying = stateInfo.IsName(animationName) && stateInfo.normalizedTime < 1.0f;
        return isPlaying;
    }

}
