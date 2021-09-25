using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveVelocity;
    [SerializeField] private string _animatorParameter;

    public UnityEvent<float> OnMoving;

    private PlayerController _controller;
    private Animator _animator;
    private float _horizontalMove = 0f;
    private float _verticalMove = 0f;
    private float _movementValue = 0;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0) 
        {
            _horizontalMove = Input.GetAxisRaw("Horizontal") * _moveVelocity;
            _movementValue = _horizontalMove;
        }          
        else
            _horizontalMove = 0;

        if (Input.GetAxisRaw("Vertical") != 0) 
        {
            _verticalMove = Input.GetAxisRaw("Vertical") * _moveVelocity;
            _movementValue = _verticalMove;
        }        
        else
            _verticalMove = 0;

        if (_horizontalMove == 0 && _verticalMove == 0)
            _movementValue = 0;      

        _animator.SetFloat(_animatorParameter, Mathf.Abs(_movementValue));

        OnMoving.Invoke(_movementValue);
    }

    private void FixedUpdate()
    {
        _controller.Move(_horizontalMove * Time.deltaTime, _verticalMove * Time.deltaTime);
    }
}