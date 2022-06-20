using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private Transform _bottomPlatformPrefab;
    [SerializeField] private Vector3 _platformOffset;
    [SerializeField] private Vector2 _mapSize;
    [SerializeField] private float _mapSizeCoefficient;
    [SerializeField] private float _mapRotationAngle;
    [SerializeField] [Range(0, 1)] private float _outlinePercent;
    [SerializeField] private float _colliderSizeY;

    private Quaternion _tileRotation;

    public Vector2 MapSize => _mapSize;
    public float MapSizeCoefficient => _mapSizeCoefficient;
    public List<Vector3> TilesPositions { get; private set; }

    private void Awake()
    {
        transform.position = new Vector3(((_mapSize.x * _mapSizeCoefficient) - _mapSizeCoefficient) / 2, 0, ((_mapSize.y * _mapSizeCoefficient) - _mapSizeCoefficient) / 2);
        _tileRotation = Quaternion.Euler(_mapRotationAngle, 0, 0);
        TilesPositions = new List<Vector3>();
        Transform platform = Instantiate(_bottomPlatformPrefab, transform.position + _platformOffset, _tileRotation);
        platform.localScale = new Vector3(_mapSize.x * _mapSizeCoefficient, _mapSize.y * _mapSizeCoefficient, platform.localScale.z);
        platform.SetParent(transform);
        GenerateMap();
    }

    private void GenerateMap() 
    {
        for (int i = 0; i < _mapSize.x; i++)
        {
            for (int j = 0; j < _mapSize.y; j++)
            {
                Vector3 tilePosition = new  Vector3(_mapSizeCoefficient * i, transform.position.y, _mapSizeCoefficient * j);
                TilesPositions.Add(tilePosition);               
                Transform newTile = Instantiate(_tilePrefab, tilePosition, _tileRotation);
                newTile.localScale = Vector3.one * (1 - _outlinePercent);
                newTile.SetParent(transform);
            }
        }

        BoxCollider collider = gameObject.AddComponent<BoxCollider>();

        collider.size = new Vector3(_mapSize.x * _mapSizeCoefficient * collider.size.x, _colliderSizeY, _mapSize.y * _mapSizeCoefficient * collider.size.z);
        collider.center = new Vector3(collider.center.x, -_colliderSizeY/2, collider.center.z);
    }
}