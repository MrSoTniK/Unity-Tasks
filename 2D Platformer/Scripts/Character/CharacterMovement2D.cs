using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement2D : MonoBehaviour
{
    [SerializeField] private float _moveVelocity;
    [SerializeField] private float _pauseTime;
    [SerializeField] private string[] _animatorParameters;

    private CharacterController2D _controller;
    private Animator _animator;
    private float _horizontalMove = 0f;
    private bool _isJump = false;

    public void OnLanding() 
    {
        _animator.SetBool(_animatorParameters[1], false);
    }  

    private void Start()
    {
        _controller = GetComponent<CharacterController2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _moveVelocity;

        _animator.SetFloat(_animatorParameters[0], Mathf.Abs(_horizontalMove));

        if (Input.GetButton("Jump"))
        {
            _isJump = true;
            _animator.SetBool(_animatorParameters[1], true);
        }
    }

    private void FixedUpdate() 
    {
        _controller.Move(_horizontalMove * Time.deltaTime, _isJump);
        _isJump = false;
    }  
}