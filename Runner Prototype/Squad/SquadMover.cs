using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SquadMover : MonoBehaviour
{
    [SerializeField] private float _maxIntervalZ;
    [SerializeField] private float _speed;

    private NavMeshAgent _agent;
    private Vector3 _nextPosition;
    private Vector3 _maxInterval;


    private void Awake()
    {       
        _maxInterval = new Vector3(0, 0, _maxIntervalZ);
        _nextPosition = _maxInterval;
        _agent = GetComponent<NavMeshAgent>();
        /*_agent.speed = _speed; */      
    }

    private void FixedUpdate()
    {       
        if(_agent.remainingDistance <= _agent.stoppingDistance) 
        {           
            /*_nextPosition += _maxInterval;*/
            _agent.Move(Vector3.forward * _speed * Time.fixedDeltaTime);
        }
    }
}