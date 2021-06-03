using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float _speed;
    private GameObject _player;
    private Vector3 _playerPosition;

    private void Start()
    {
        _player = GameObject.Find("Player");
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
        if(body.name == "Player") 
        {
            Destroy(_player);
            Debug.Log("Game Over");
        }   
        
        if(body.tag == "Projectile") 
        {
            Destroy(gameObject);
            Enemy.EnemiesSpawn.Decrease();
        }
    }
}
