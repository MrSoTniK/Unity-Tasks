using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _speed;
    private GameObject _player;
    private Vector3 _playerPosition;

    private void Start()
    {
        _player = Player.GetPlayer();
    }

    private void Update()
    {
        if(_player != null) 
        {
            _playerPosition = _player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, _playerPosition, _speed * Time.deltaTime);
        }     
    }
    private void OnTriggerEnter(Collider body)
    {       
        if(body.gameObject == _player) 
        {
            Destroy(_player);
            Debug.Log("Game Over");
        }   
        
        if(body.gameObject.name.Contains(_projectile.name))
        {
            Destroy(gameObject);
            Enemy.EnemiesSpawn.Decrease();
        }
    }
}