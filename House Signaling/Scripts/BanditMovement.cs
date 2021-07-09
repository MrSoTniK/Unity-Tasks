using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BanditMovement : MonoBehaviour
{   
    [SerializeField] private WaypointsPool _pool;

    private Waypoint[] _waypoints;
    private Animator _animator;
    private bool _isStopped;

    public int WaypointIndex { get; private set; }

    private void Start()
    {
        _waypoints = _pool.GetComponentsInChildren<Waypoint>();
        _animator = GetComponent<Animator>();
        WaypointIndex = 0;
        _isStopped = false;
    }
  
    private void Update()
    {
        if (_isStopped == false)
        {
            Vector3 currentPosition = transform.position;
            Vector3 targetPosition = _waypoints[WaypointIndex].transform.position;
            transform.LookAt(targetPosition);
            transform.position = Vector3.MoveTowards(currentPosition, targetPosition, _waypoints[WaypointIndex].Speed * Time.deltaTime);

            Vector3 reachedPosition = transform.position;
            if (reachedPosition == targetPosition)
            {              
                TryToSetPause(WaypointIndex);                             
            }
        }      

        if (WaypointIndex >= _waypoints.Length)
            Destroy(gameObject);
    }

   private void TryToSetPause(int index) 
   {
        if (_waypoints[index].PauseTime > 0)
            StartCoroutine(RunPause(_waypoints[index].PauseTime));
        else
            WaypointIndex++; 
    }
    
    private IEnumerator RunPause(float timeValue) 
    {
        _isStopped = true;
        yield return new WaitForSeconds(timeValue);
        WaypointIndex++;
        _isStopped = false;
    }
}
