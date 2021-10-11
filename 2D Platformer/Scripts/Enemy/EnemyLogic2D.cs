using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyController2D))]
public class EnemyLogic2D : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _pauseBetweenTouches;
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private Transform _playerPosition;

    public UnityEvent<int> OnTouchEvent;
    private EnemyMovementController2D _controller;
    private int _currentId;
    private bool _isPause;

    public void TryToChase(bool isChasing) 
    {      
        if (!isChasing)
            _controller.MoveToDestination(true, _currentId, _waypoints[_currentId].position.x, _waypoints.Length, out _currentId);
        else                   
            _controller.MoveToDestination(false, _currentId, _playerPosition.position.x, _waypoints.Length, out _currentId);                  
    }

    private void Start()
    {
        _controller = GetComponent<EnemyMovementController2D>();
        _isPause = false;
    }

    private void OnTriggerStay2D(Collider2D body)
    {
        if (body.TryGetComponent<Player>(out Player player) && !_isPause) 
        {
            OnTouchEvent.Invoke(_damage);
            StartCoroutine(StartPause());
        }
    }   

    IEnumerator StartPause() 
    {
        _isPause = true;
        yield return new WaitForSeconds(_pauseBetweenTouches);
        _isPause = false;
    }
}
