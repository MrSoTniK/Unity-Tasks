using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _pauseBetweenEnemiesSpawn;
    [SerializeField] private Transform[] _spawnZones;

    private float _elapsedTime = 0;

    private void Start()
    {
        Initialize(_enemyPrefab);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _pauseBetweenEnemiesSpawn) 
        {
            if(TryGetObject(out GameObject enemy)) 
            {
                _elapsedTime = 0;
                int spawnZoneNumber = Random.Range(0, _spawnZones.Length);
                SetEnemy(enemy, _spawnZones[spawnZoneNumber].position);
            }                    
        }
    }

    private void SetEnemy(GameObject enemy, Vector3 spwanPoint) 
    {
        enemy.SetActive(true);
        enemy.transform.position = spwanPoint;
    }
}
