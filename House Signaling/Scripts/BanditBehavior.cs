using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBehavior : MonoBehaviour
{
    private float _xPos;
    private float _yPos;
    private float _zPos;
    private float _speed;
    private Animator _animator;
    private int _wayPointNumberXandZ;
    private bool _isStopped;

    private void Start()
    {     
        _animator = gameObject.GetComponent<Animator>();
        _wayPointNumberXandZ = 0;
        ChooseCoordinates();
        _isStopped = false;
    }
  
    private void Update()
    {
        if (_isStopped == false)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = new Vector3(_xPos, _yPos, _zPos);
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, _speed);

            Vector3 reachedPosition = transform.position;
            if (reachedPosition == targetPosition)
            {              
                ChooseCoordinates();
            }
        }
    }

    private void ChooseCoordinates() 
    {
        switch (_wayPointNumberXandZ)
        {
            case 0:
                _xPos = -2.208f;
                _yPos = 0.115f;
                _zPos = 12.18f;
                _animator.SetBool("IsWalk", true);
                _speed = 0.04f;
                break;
            case 1:
                _animator.SetTrigger("TurnRight");
                _xPos = -2.308f;
                _yPos = 0.115f;
                _zPos = 11.489f;                
                _speed = 0.02f;
                break;
            case 2:                           
                _animator.SetBool("IsWalk", false);
                _animator.SetBool("IsCrouchIdle", true);
                _animator.SetBool("IsInteraction", true);
                StartCoroutine(RunPause(7f));
                break;
            case 3:                
                _animator.SetBool("IsInteraction", false);
                _animator.SetBool("IsCrouchIdle", false);               
                _animator.SetBool("IsWalk", true);
                StartCoroutine(RunPause(1f));
                _xPos = -2.308f;
                _yPos = 0.115f;
                _zPos = -0.117f;
                _speed = 0.04f;
                break;
            case 4:
                _animator.SetBool("IsWalk", false);
                _animator.SetBool("IsCrouchIdle", true);               
                StartCoroutine(RunPause(7f));
                break;
            case 5:
                _animator.SetBool("IsInteraction", false);
                _animator.SetBool("IsCrouchIdle", true);
                _animator.SetBool("IsCrouchMove", true);
                _xPos = -2.308f;
                _yPos = 0.115f;
                _zPos = -2.3f;
                _speed = 0.02f;
                break;
            case 6:
                _xPos = -0.71f;
                _yPos = 0.115f;
                _zPos = -4.42f;
                _speed = 0.02f;
                break;
            case 7:
                _xPos = -0.71f;
                _yPos = 0.115f;
                _zPos = -8.35f;
                _speed = 0.02f;
                break;
            case 8:
                _xPos = -3.24f;
                _yPos = 0.115f;
                _zPos = -8.35f;
                _speed = 0.02f;
                break;
            case 9:
                _animator.SetBool("IsCrouchMove", false);
                _animator.SetBool("IsCrouchIdle", false);
                _animator.SetBool("IsWalk", true);
                _animator.SetBool("IsRun", true);
                StartCoroutine(RunPause(1f));
                _xPos = -3.24f;
                _yPos = 0.115f;
                _zPos = -4.1f;
                _speed = 0.1f;
                break;
            case 10:
                _xPos = -2.31f;
                _yPos = 0.115f;
                _zPos = -2.14f;
                _speed = 0.1f;
                break;
            case 11:
                _xPos = -2.31f;
                _yPos = 0.115f;
                _zPos = -0.07f;
                _speed = 0.1f;
                break;
            case 12:
                _xPos = -2.308f;
                _yPos = 0.115f;
                _zPos = 11.489f;
                _speed = 0.1f;
                break;
            case 13:
                _xPos = -22.97f;
                _yPos = 0.115f;
                _zPos = 12.18f;
                _speed = 0.1f;
                break;
            case 14:
                _animator.SetBool("IsRun", false);
                _animator.SetBool("IsWalk", false);               
                _isStopped = true;
                break;
        }
        _wayPointNumberXandZ++;
    }     
    
    IEnumerator RunPause(float timeValue) 
    {
        _isStopped = true;
        yield return new WaitForSeconds(timeValue);
        _isStopped = false;
        StopCoroutine(RunPause(timeValue));
    }
}
