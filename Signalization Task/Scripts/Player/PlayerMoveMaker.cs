using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMoveDefiner))]
public class PlayerMoveMaker : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private string _movementParameterName;

    private PlayerMoveDefiner _playerDefiner;
    private float _horizontalMove;
    private float _verticalMove;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _playerDefiner = GetComponent<PlayerMoveDefiner>(); 
        _horizontalMove = 0f;
        _verticalMove = 0f;
    }
    
    private void FixedUpdate()
    {
        float movement = 0;
        _horizontalMove = Input.GetAxisRaw("Horizontal") * _moveSpeed * Time.deltaTime;
        _verticalMove = Input.GetAxisRaw("Vertical") * _moveSpeed * Time.deltaTime;

        if (_horizontalMove != 0 || _verticalMove != 0)
            movement = _moveSpeed;

        _animator.SetFloat(_movementParameterName, Mathf.Abs(movement));
        _playerDefiner.Move(_horizontalMove, _verticalMove);
    }
}