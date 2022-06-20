using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class EnemyPatrolState : State
{
    [SerializeField] private float _inaccuracy;
    [SerializeField] private NavMeshAgent _agent;

    private Enemy _enemy;
    private List<Vector3> _patrolPoints;
    private Vector3 _currentPoint;
    private int _currentPointID;

    private void OnEnable()
    {
        _enemy = GetComponent<Enemy>();
        _patrolPoints = _enemy.PatrolPoints;
        _currentPoint = _patrolPoints[0];
        _currentPointID = 0;
        _agent.SetDestination(_currentPoint);
    }

    private void Update()
    {
        TryToChangePoint();
    }

    private void TryToChangePoint() 
    {
        if (Vector3.Distance(_currentPoint, transform.position) <= _inaccuracy)
        {
            if (_currentPointID < _patrolPoints.Count - 1)
                _currentPointID++;
            else
                _currentPointID = 0;

            _currentPoint = _patrolPoints[_currentPointID];
            _agent.SetDestination(_currentPoint);
        }
    }
}