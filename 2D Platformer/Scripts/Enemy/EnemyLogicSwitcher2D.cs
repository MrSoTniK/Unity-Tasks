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
        {
            OnPursueEvent.Invoke(true);
            _isPlayerNear = true;
        }
        else 
        {
            OnPursueEvent.Invoke(false);
            _isPlayerNear = false;
        }

        if (!_isPlayerNear && _wasPlayerNear)
            OnPlayerLost.Invoke();
    }

    public void OnPlayerDeath()
    {
        _agroRange = -1;
    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }
}