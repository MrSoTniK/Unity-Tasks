using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshGenerator : MonoBehaviour
{
    [SerializeField] private ObstaclesGenerator _obstaclesGenerator;
    [SerializeField] private float _checkingDistance;

    private NavMeshSurface _navMesh;

    public List<Vector3> FreeTilesWithNavMesh { get; private set; }

    public UnityAction NavMeshGenerated;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshSurface>();        
    }

    private void OnEnable()
    {
        _obstaclesGenerator.ObstaclesGenerated += GenerateNavMesh;
    }

    private void OnDisable()
    {
        _obstaclesGenerator.ObstaclesGenerated -= GenerateNavMesh;
    }

    private void GenerateNavMesh() 
    {
        _navMesh.BuildNavMesh();
        CheckTilesForNavMesh();
        NavMeshGenerated?.Invoke();
    }

    private void CheckTilesForNavMesh() 
    {
        FreeTilesWithNavMesh = _obstaclesGenerator.FreeTilesPositions;
        Vector3[] freeTiles = FreeTilesWithNavMesh.ToArray();
        foreach (var tilePos in freeTiles) 
        {
            NavMeshHit hit = new NavMeshHit();
            if (!NavMesh.SamplePosition(tilePos, out hit, _checkingDistance, NavMesh.AllAreas))
                FreeTilesWithNavMesh.Remove(tilePos);
        }       
    }
}