using UnityEngine;
using UnityEngine.AI;

public class EnemyPursueState : State
{
    [SerializeField] protected NavMeshAgent Agent;
    [SerializeField] protected Player Player;

    protected void Update()
    {
        Agent.SetDestination(Player.transform.position);
    }
}