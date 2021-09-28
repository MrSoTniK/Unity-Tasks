using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterCombat2D : MonoBehaviour
{
    [SerializeField] private KeyCode _attackKey;
    [SerializeField] private string  _attackAnimationParameter;
    [SerializeField] private string _runAnimationParameter;
    [SerializeField] private string _jumpAnimationParameter;
    [SerializeField] private float _speedMinValueAccurance;
    [SerializeField] private float _timeOfAttackAnimation;
    [SerializeField] private Collider2D _weaponCollider;

    private Animator _animator;   

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _weaponCollider.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_attackKey))
            Attack();
    }

    private void Attack() 
    {
        float speedValue;
        bool isJumping;

        speedValue = _animator.GetFloat(_runAnimationParameter);
        isJumping = _animator.GetBool(_jumpAnimationParameter);

        if(speedValue < _speedMinValueAccurance && !isJumping) 
        {
            _animator.SetTrigger(_attackAnimationParameter);
            StartCoroutine(SetWeaponColliderActive());
        }
    }

    private IEnumerator SetWeaponColliderActive() 
    {
        _weaponCollider.enabled = true;
        yield return new WaitForSeconds(_timeOfAttackAnimation);
        _weaponCollider.enabled = false;
    }
}
