using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BanditMovementPathDeterminant : MonoBehaviour
{   
    [SerializeField] private Waypoint[] _waypoints; 

    private Animator _animator;
    private bool _isStopped;
    [SerializeField] private int _waypointIndex;

    private void Start()
    {     
        _animator = GetComponent<Animator>();
        _waypointIndex = 0;
        _isStopped = false;
    }
  
    private void Update()
    {
        if (_isStopped == false)
        {
            _waypoints[_waypointIndex].SetAnimations(_animator);
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = _waypoints[_waypointIndex].Coordinate;
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, _waypoints[_waypointIndex].Speed * Time.deltaTime);

            Vector3 reachedPosition = transform.position;
            if (reachedPosition == targetPosition)
            {              
                TryToSetPause(_waypointIndex);              
                _waypointIndex++;
            }
        }      

        if (_waypointIndex >= _waypoints.Length)
            Destroy(gameObject);
    }

   private void TryToSetPause(int index) 
   {
        if (_waypoints[index].IsPauseExist == true)
            StartCoroutine(RunPause(_waypoints[index].PauseTime));
   }
    
    private IEnumerator RunPause(float timeValue) 
    {
        _isStopped = true;
        yield return new WaitForSeconds(timeValue);
        _isStopped = false;
    }
}
