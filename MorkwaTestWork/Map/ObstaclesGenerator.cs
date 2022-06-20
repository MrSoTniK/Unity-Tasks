using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(MapGenerator))]
public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] private Transform _obstaclePrefab;
    [SerializeField] [Range(0, 1)] private float _obstaclesPercent;
    [SerializeField] private float _offsetY;
    [SerializeField] private ObstaclesPool _obstaclesPool;

    private List<Vector3> _tilesCoordinates;
    private Queue<Vector3> _shuffledTilesCoordinates;
    private MapGenerator _mapGenerator;
    private Vector3 _offset;

    public List<Vector3> FreeTilesPositions;

    public UnityAction ObstaclesGenerated;

    private void Awake()
    {
        _mapGenerator = GetComponent<MapGenerator>();
        _offset = new Vector3(0, _offsetY, 0);
        FreeTilesPositions = _mapGenerator.TilesPositions;
	}

    private void Start()
    {
        GenerateObstacles();       
    }

    private void GenerateObstacles() 
	{
		_tilesCoordinates = new List<Vector3>();   
        Transform newPool = Instantiate(_obstaclesPool, transform.position, Quaternion.identity).GetComponent<Transform>();
        newPool.SetParent(transform);

        for (int i = 0; i < _mapGenerator.MapSize.x; i++)       
            for (int j = 0; j < _mapGenerator.MapSize.y; j++)          
                _tilesCoordinates.Add(new Vector3(i * _mapGenerator.MapSizeCoefficient,  transform.position.y, j * _mapGenerator.MapSizeCoefficient));

        _shuffledTilesCoordinates = new Queue<Vector3>(Shuffler.ShuffleArray<Vector3>(_tilesCoordinates.ToArray()));

        int obstaclesQuantity = (int)(_mapGenerator.MapSize.x * _mapGenerator.MapSize.y * _obstaclesPercent);
        for(int i = 0; i < obstaclesQuantity; i++) 
        {
            Vector3 coord = GetRandomCoordinate();			
			Transform newObstacle = Instantiate(_obstaclePrefab, coord + _offset, Quaternion.identity);
			newObstacle.SetParent(newPool);					        
        }

        ObstaclesGenerated?.Invoke();
    }

	private Vector3 GetRandomCoordinate()
	{
		Vector3 randomCoordinate = _shuffledTilesCoordinates.Dequeue();
        FreeTilesPositions.Remove(randomCoordinate);
        _shuffledTilesCoordinates.Enqueue(randomCoordinate);
		return randomCoordinate;
	}
}