using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BanditMovement))]
public class BanditAnimator : MonoBehaviour
{
    [SerializeField] private WaypointsPool _pool;
    [SerializeField] private AnimationsPool _animPool;

    private Waypoint[] _waypoints;
    private int _currentWaypointIndex;
    private Animator _animator;
    private BanditMovement _movement;
    private Animations[] _animations;

    private void Start()
    {
        _animations = _animPool.GetComponentsInChildren<Animations>();
        _waypoints = _pool.GetComponentsInChildren<Waypoint>();
        _currentWaypointIndex = 0;
        _animator = GetComponent<Animator>();
        _movement = GetComponent<BanditMovement>();
        _currentWaypointIndex = SetAnimations(_currentWaypointIndex);
    }

    private void Update()
    {
        if(_currentWaypointIndex < _animations.Length) 
        {
            if (_movement.WaypointIndex == _animations[_currentWaypointIndex].WaypointId)
                _currentWaypointIndex = SetAnimations(_currentWaypointIndex);
        }                                
    }

    private int SetAnimations(int index) 
    {
        if(_animations[index].AnimationsToSetInactive.Length > 0) 
        {
            foreach (var animationName in _animations[index].AnimationsToSetInactive)
            {
                _animator.SetBool(animationName, false);
            }
        }       

        if(_animations[index].AnimationsToSetActive.Length > 0) 
        {
            foreach (var animationName in _animations[index].AnimationsToSetActive)
            {
                _animator.SetBool(animationName, true);
            }
        }
        index = index + 1;

        return index;     
    }
}
