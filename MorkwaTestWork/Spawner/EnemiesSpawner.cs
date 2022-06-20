using UnityEngine;
using UnityEngine.Events;

public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] private EnemyPatrolPointsGenerator _generator;
    [SerializeField] private Enemy[] _enemies;

    public UnityAction EnemiesWereSpawned;

    private void OnEnable()
    {
        _generator.PointsWereGenerated += SpawnEnemies;
    }

    private void OnDisable()
    {
        _generator.PointsWereGenerated -= SpawnEnemies;
    }

    private void SpawnEnemies() 
    {
        for(int i = 0; i < _enemies.Length; i++)
        {
            _enemies[i].gameObject.SetActive(true);
            _enemies[i].GetPatrolPoint(_generator.FirstPatrolPoints[i]);
            _enemies[i].GetPatrolPoint(_generator.SecondPatrolPoints[i]);
            _enemies[i].transform.position = _generator.FirstPatrolPoints[i];
        }

        EnemiesWereSpawned?.Invoke();
    }
}