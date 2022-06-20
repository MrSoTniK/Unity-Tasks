using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyPatrolPointsGenerator : MonoBehaviour
{
    [SerializeField] private PlayerPointsGenerator _playerPointsGenerator;
    [SerializeField] private float _pointsPatrolPairsQuantity;
    [SerializeField] private float _minDistanceBetweenPointsPair;
    [SerializeField] private Vector3 _offset;

    public List<Vector3> FreeTiles { get; private set; }
    public List<Vector3> FirstPatrolPoints { get; private set; }
    public List<Vector3> SecondPatrolPoints { get; private set; }

    public UnityAction PointsWereGenerated;

    private void OnEnable()
    {
        _playerPointsGenerator.PointsWereGenerated += GenerateEnemiesPatrolPoints;
    }

    private void OnDisable()
    {
        _playerPointsGenerator.PointsWereGenerated -= GenerateEnemiesPatrolPoints;
    }

    private void GenerateEnemiesPatrolPoints() 
    {
        FreeTiles = _playerPointsGenerator.FreeTiles;
        FirstPatrolPoints = new List<Vector3>();
        SecondPatrolPoints = new List<Vector3>();
        System.Random random = new System.Random();      

        for (int i = 0; i < _pointsPatrolPairsQuantity; i++) 
        {
            Vector3 firstPoint = Vector3.zero;
            Vector3 secondPoint = Vector3.zero;

            while (Vector3.Distance(firstPoint, secondPoint) < _minDistanceBetweenPointsPair) 
            {
                GeneratePoint(random, out firstPoint);
                GeneratePoint(random, out secondPoint);
            }
            FirstPatrolPoints.Add(firstPoint + _offset);
            SecondPatrolPoints.Add(secondPoint + _offset);
        }

        PointsWereGenerated?.Invoke();
    }

    private void GeneratePoint(System.Random random, out Vector3 point)
    {
        int randomIndex = random.Next(0, FreeTiles.Count);
        point = FreeTiles[randomIndex];
        FreeTiles.Remove(point);
    }
}