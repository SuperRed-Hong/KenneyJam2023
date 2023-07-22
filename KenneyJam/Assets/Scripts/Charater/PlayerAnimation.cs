using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    private void Awake()
    {
        animator = GetComponent<Animator>();    
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        animator.SetFloat("input.x" , characterController.inputDirection.x);
        animator.SetFloat("input.y", characterController.inputDirection.y);

    }
}
