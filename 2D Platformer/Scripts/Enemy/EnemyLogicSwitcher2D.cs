using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyLogicSwitcher2D : MonoBehaviour
{
    [SerializeField] private float _agroRange;
    [SerializeField] private Transform _playerPosition;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    public BoolEvent OnPursueEvent;
    public UnityEvent OnPlayerLost;

    private bool _isPlayerNear = false;
    private bool _wasPlayerNear = false;

    public void CheckDistanceToPlayer()
    {      
        float distanceToPlayer = Vector2.Distance(transform.position, _playerPosition.position);

        _wasPlayerNear = _isPlayerNear;

      if (distanceToPlayer <= _agroRange)       
            _isPlayerNear = true;      
        else       
            _isPlayerNear = false;       

        OnPursueEvent.Invoke(_isPlayerNear);

        if (!_isPlayerNear && _wasPlayerNear)
            OnPlayerLost.Invoke();
    }

    private void Start()
    {
        _playerPosition.OnPlayerDeath += OnPlayerDeath;
    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }

    private void OnPlayerDeath()
    {
        _agroRange = -1;
    }
}
