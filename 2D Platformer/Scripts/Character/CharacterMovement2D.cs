using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement2D : MonoBehaviour
{
    [SerializeField] private float _moveVelocity;
    [SerializeField] private float _pauseTime;

    private CharacterController2D _controller;
    private Animator _animator;
    private float _horizontalMove = 0f;
    private bool _isJump = false;

    public void OnLanding() 
    {
        _animator.SetBool(AnimatorCharacter.Params.IsJumping, false);
    }  

    private void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw(InputManager.Axes.Horizontal) * _moveVelocity;

        _animator.SetFloat(AnimatorCharacter.Params.Speed, Mathf.Abs(_horizontalMove));

        if (Input.GetButton(InputManager.Axes.Jump))
        {
            _isJump = true;
            _animator.SetBool(AnimatorCharacter.Params.IsJumping, true);
        }
    }

    private void FixedUpdate() 
    {
        _controller.Move(_horizontalMove * Time.deltaTime, _isJump);
        _isJump = false;
    }  
}
