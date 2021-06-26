using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Player _player;
    private Vector3 _playerPosition;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (_player != null)
        {
            _playerPosition = _player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition, _speed * Time.deltaTime);
        }
    }
}
