using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private bool _isTrigerExist;
    [SerializeField] private bool _isAnimationExist;
    [SerializeField] private bool _isPauseExist;

    [SerializeField] private string[] _animationsToSetActive;
    [SerializeField] private string[] _animationsToSetInactive;

    [SerializeField] private string _trigerName;
    
    [SerializeField] private float _pauseTime;
    [SerializeField] private float _speed;

    [SerializeField] private Vector3 _coordinate;

    public Vector3 Coordinate { get; private set; }
    public float Speed { get; private set; }
    public float PauseTime { get; private set; }
    public bool IsPauseExist { get; private set; }

    public void Start()
    {
        Coordinate = _coordinate;
        Speed = _speed;
        IsPauseExist = _isPauseExist;
        PauseTime = _pauseTime;
    }

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
}
