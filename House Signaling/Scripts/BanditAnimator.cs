using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BanditMovement))]
public class BanditAnimator : MonoBehaviour
{
    [SerializeField] private WaypointsPool _pool;

    private Waypoint[] _waypoints;
    private int _currentWaypointIndex;
    private Animator _animator;
    private BanditMovement _movement;

    private void Start()
    {
        _waypoints = _pool.GetComponentsInChildren<Waypoint>();
        _currentWaypointIndex = 0;
        _animator = GetComponent<Animator>();
        _movement = GetComponent<BanditMovement>();
        SetAnimations(_movement.WaypointIndex);
    }

    private void Update()
    {
        if(_movement.WaypointIndex > _currentWaypointIndex) 
        {
            SetAnimations(_movement.WaypointIndex);
            _currentWaypointIndex = _movement.WaypointIndex;
        }                       
    }

    private void SetAnimations(int index) 
    {
        if(_waypoints[index].AnimationsToSetInactive.Length > 0) 
        {
            foreach (var animationName in _waypoints[index].AnimationsToSetInactive)
            {
                _animator.SetBool(animationName, false);
            }
        }       

        if(_waypoints[index].AnimationsToSetActive.Length > 0) 
        {
            foreach (var animationName in _waypoints[index].AnimationsToSetActive)
            {
                _animator.SetBool(animationName, true);
            }
        }       
    }
}
