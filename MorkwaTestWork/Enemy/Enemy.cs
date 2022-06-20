using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Entity
{
    [SerializeField] private StateMachine _stateMachine;
    [SerializeField] private EnemiesSpawner _spawner;

    public List<Vector3> PatrolPoints { get; private set; }

    protected override void Awake()
    {
        CurrentHealth = MaxHealth;
        PatrolPoints = new List<Vector3>();
    }

    private void OnEnable()
    {
        _spawner.EnemiesWereSpawned += TurnOnStateMachine;
    }

    private void OnDisable()
    {
        _spawner.EnemiesWereSpawned -= TurnOnStateMachine;
    }

    public void GetPatrolPoint(Vector3 point) 
    {
        PatrolPoints.Add(point);
    }

    private void TurnOnStateMachine() 
    {
        _stateMachine.enabled = true;
    }
}