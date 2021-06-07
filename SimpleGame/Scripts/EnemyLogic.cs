using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float _speed;
    private GameObject _player;
    private Vector3 _playerPosition;

    private GameObject GetPlayer() 
    {
        GameObject player = null;
        GameObject[] sceneGameObjects = FindObjectsOfType<GameObject>();

        foreach (var item in sceneGameObjects)
        {
            if (item.TryGetComponent<Player>(out Player playerUnit))
            {
                player = item;
                Debug.Log("Success");
            }
        }

        return player;
    }

    private void Start()
    {
        _player = GetPlayer();
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
        if(body.TryGetComponent<Player>(out Player player)) 
        {
            Destroy(player.gameObject);
            Debug.Log("Game Over");
        }   
        
        if(body.TryGetComponent<ProjectileDeletion>(out ProjectileDeletion projectile))
        {
            Destroy(gameObject);
            Enemy.EnemiesSpawn.Decrease();
        }
    }
}