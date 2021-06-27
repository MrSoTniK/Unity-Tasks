using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private string[] _animationsToSetActive;
    [SerializeField] private string[] _animationsToSetInactive;
    [SerializeField] private string _trigerName;
    [SerializeField] private bool _isTrigerExist;
    [SerializeField] private bool _isAnimationExist;
    [SerializeField] private Vector3 _coordinate;
    [SerializeField] private float _speed;
    [SerializeField] private bool _isPauseExist;
    [SerializeField] private float _pauseTime;

    public void SetAnimations(Animator animator) 
    {
        if (_isAnimationExist && _animationsToSetInactive.Length != 0)
        {
            for (int i = 0; i < _animationsToSetInactive.Length; i++)
                animator.SetBool(_animationsToSetInactive[i], false);
        }

        if (_isAnimationExist && _animationsToSetActive.Length != 0) 
        {
            for (int i = 0; i < _animationsToSetActive.Length; i++)
                animator.SetBool(_animationsToSetActive[i], true);
        }

        if(_isTrigerExist && _trigerName != null) 
        {
            animator.SetTrigger(_trigerName);
        }
    }    

    public Vector3 GetCoordinate() 
    {
        return _coordinate;
    }

    public float GetSpeed() 
    {
        return _speed;
    }

    public bool CheckPause() 
    {
        return _isPauseExist;
    }

    public float GetPauseTime() 
    {
        return _pauseTime;
    }
}
