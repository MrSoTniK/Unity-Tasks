using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPointsGenerator : MonoBehaviour
{
    [SerializeField] private Transform _exitPrefab;
    [SerializeField] private float _minDistanceBetweenPoints;
    [SerializeField] private NavMeshGenerator _navMeshGenerator;

    private Vector3 _spawnPoint;
    private Vector3 _exitPoint;

    public Vector3 SpawnPoint => _spawnPoint;
    public List<Vector3> FreeTiles { get; private set; }

    public UnityAction PointsWereGenerated;

    private void OnEnable()
    {
        _navMeshGenerator.NavMeshGenerated += GenerateExitAndSpawnPoints;
    }

    private void OnDisable()
    {
        _navMeshGenerator.NavMeshGenerated -= GenerateExitAndSpawnPoints;
    }

    private void GenerateExitAndSpawnPoints() 
    {
        FreeTiles = _navMeshGenerator.FreeTilesWithNavMesh;
        System.Random random = new System.Random();
        GeneratePoint(random, out _spawnPoint);
        GeneratePoint(random, out _exitPoint);

        while(Vector3.Distance(_spawnPoint, _exitPoint) < _minDistanceBetweenPoints) 
        {
            FreeTiles.Add(_exitPoint);
            GeneratePoint(random, out _exitPoint);
        }

        Transform exit = Instantiate(_exitPrefab, _exitPoint, Quaternion.identity);
        exit.SetParent(transform);
        PointsWereGenerated?.Invoke();
    }

    private void GeneratePoint(System.Random random, out Vector3 point) 
    {
        int randomIndex = random.Next(0, FreeTiles.Count);
        point = FreeTiles[randomIndex];
        FreeTiles.Remove(point);
    }
}