using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private Transform[] _obstaclePrefabs;
	[SerializeField] private Vector2 _mapSize;
	[SerializeField] [Range(0, 1)] private float outlinePercent;
	[SerializeField] private float _mapSizeCoefficient;
	[SerializeField] private float _tileCoefficient;
	[SerializeField] private int _obstaclesPoistionZ;
	[SerializeField] private Vector2 _offset;
	[SerializeField] [Range(0, 1)] private float _obstaclesPercent;

	public UnityEvent<List<Vector3>> OnMapGenerated;
	public UnityEvent OnNavMeshGeneration;

	private List<Coordinate> _tilesCoordinates;
	private Queue<Coordinate> _shuffledTilesCoordinates;
	private Coordinate _mapCentre;
	private List<Vector3> _freeTiles;

	private void Start()
    {
		GenerateMap();
		OnMapGenerated.Invoke(_freeTiles);		
	}
   
    private void GenerateMap()
	{
		_mapCentre = new Coordinate((int)_mapSize.x / 2, (int)_mapSize.y / 2);
		_tilesCoordinates = new List<Coordinate>();
		_freeTiles = new List<Vector3>();
		List<Transform> _tiles = new List<Transform>();

		for (int i = 0; i < _mapSize.x; i++)
		{
			for(int j = 0; j < _mapSize.y; j++)
			{
				_tilesCoordinates.Add(new Coordinate(i, j));
			}
		}
		_shuffledTilesCoordinates = new Queue<Coordinate>(Utility.ShuffleArray(_tilesCoordinates.ToArray()));
		
		for(int i = 0; i < _mapSize.x; i++)
		{
			for(int j = 0; j < _mapSize.y; j++)
			{
				Vector3 tilePosition = CalculatePosition(i, j, 0);
				_freeTiles.Add(tilePosition);
				Transform newTile = Instantiate(_tilePrefab, tilePosition, Quaternion.Euler(Vector3.right)) as Transform;			
				newTile.localScale = Vector3.one * (1- outlinePercent);
				newTile.transform.SetParent(transform);
				_tiles.Add(newTile);
			}
		}
		// Add BoxCollider2D:
		// gameObject.AddComponent<BoxCollider2D>().size = new Vector2(_mapSize.x * _tileCoefficient * GetComponent<BoxCollider2D>().size.x, _mapSize.y * _tileCoefficient * GetComponent<BoxCollider2D>().size.y);
		
		int obstaclesQuantity = (int)(_mapSize.x * _mapSize.y * _obstaclesPercent);
		System.Random index = new System.Random();

		bool[,] obstacleMap = new bool[(int)_mapSize.x, (int)_mapSize.y];
		int currentObstacleQuantity = 0;

		for (int i =0; i < obstaclesQuantity; i ++) 
		{
			Coordinate randomCoord = GetRandomCoordinate();
			obstacleMap[randomCoord.X, randomCoord.Y] = true;
			currentObstacleQuantity++;

			if (randomCoord != _mapCentre && CheckIfMapFullyExcessible(obstacleMap, currentObstacleQuantity))
			{
				Vector3 obstaclePosition = CalculatePosition(randomCoord.X, randomCoord.Y, _obstaclesPoistionZ);
				_freeTiles.Remove(new Vector3(obstaclePosition.x, obstaclePosition.y, 0));
				int randomIndex = index.Next(0, _obstaclePrefabs.Length);
				Transform newObstacle = Instantiate(_obstaclePrefabs[randomIndex], obstaclePosition, Quaternion.identity) as Transform;
				newObstacle.transform.SetParent(transform);
				Transform tileForDelete = _tiles.First(tile => tile.position == new Vector3(newObstacle.position.x, newObstacle.position.y, 0));
				Destroy(tileForDelete.gameObject);
			}
			else 
			{
				obstacleMap[randomCoord.X, randomCoord.Y] = false;
				currentObstacleQuantity--;
			}
		}

		GenerateBoundaries();
		OnNavMeshGeneration.Invoke();
	}

	private bool CheckIfMapFullyExcessible(bool[,] obstacleMap, int currentObstacleCount)
	{
		bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
		Queue<Coordinate> queue = new Queue<Coordinate>();
		queue.Enqueue(_mapCentre);
		mapFlags[_mapCentre.X, _mapCentre.Y] = true;

		int accessibleTileCount = 1;

		while (queue.Count > 0)
		{
			Coordinate tile = queue.Dequeue();

			for (int x = -1; x <= 1; x++)
			{
				for (int y = -1; y <= 1; y++)
				{
					int neighbourX = tile.X + x;
					int neighbourY = tile.Y + y;
					if (x == 0 || y == 0)
					{
						if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
						{
							if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
							{
								mapFlags[neighbourX, neighbourY] = true;
								queue.Enqueue(new Coordinate(neighbourX, neighbourY));
								accessibleTileCount++;
							}
						}
					}
				}
			}
		}
		int targetAccessibleTileCount = (int)(_mapSize.x * _mapSize.y - currentObstacleCount);
		return targetAccessibleTileCount == accessibleTileCount;
	}
	
	private Vector3 CalculatePosition(int x, int y, int z)
	{
		return new Vector3(-_mapSize.x * _mapSizeCoefficient + _tileCoefficient * x + _offset.x, -_mapSize.y * _mapSizeCoefficient + _tileCoefficient * y + _offset.y, z);	
	}
	
	private Coordinate GetRandomCoordinate()
	{
		Coordinate randomCoordinate = _shuffledTilesCoordinates.Dequeue();
		_shuffledTilesCoordinates.Enqueue(randomCoordinate);
		return randomCoordinate;		
	}
	
	private void GenerateBoundaries()
	{
		float boundaryX = -_mapSize.x *( _mapSizeCoefficient - _tileCoefficient) + _offset.x;
		float boundaryY = -_mapSize.y *( _mapSizeCoefficient - _tileCoefficient) + _offset.y;
		
		GenerateBoundary(boundaryX, 0);
		GenerateBoundary(-boundaryX, 0);
		GenerateBoundary(0, boundaryY);
		GenerateBoundary(0, -boundaryY);
	}
	
	private void GenerateBoundary(float x, float y)
	{
		Vector3 boundaryPosition = new Vector3(x, y, _obstaclesPoistionZ);
		Transform boundary = Instantiate(_tilePrefab, boundaryPosition, Quaternion.Euler(Vector3.right));
		boundary.SetParent(transform);	
		boundary.gameObject.GetComponent<SpriteRenderer>().color = Color.black;
		boundary.gameObject.AddComponent<BoxCollider2D>();
		NavMeshModifier modifier = boundary.gameObject.GetComponent<NavMeshModifier>();
		modifier.area = 1;
		Vector3 boundaryScale =  boundary.localScale;
		if(x == 0)
		{
			boundaryScale.x =  _mapSize.x;
			boundary.localScale = boundaryScale;
			
		}	    
		if(y == 0)
		{
			boundaryScale.y =  _mapSize.y + Mathf.Abs(_offset.y);
			boundary.localScale = boundaryScale;
		}
	}
	
	private struct Coordinate
	{
		public int X;
		public int Y;
		
		public Coordinate(int x, int y)
		{
			X = x;
			Y = y;
		}

		public static bool operator == (Coordinate c1, Coordinate c2)
		{
			return c1.X == c2.X && c1.Y == c2.Y;
		}

		public static bool operator !=(Coordinate c1, Coordinate c2)
		{
			return !(c1 == c2);
		}

		public override bool Equals(object obj)
		{
			if (obj is Coordinate coord)
			{
				return this == coord;
			}

			return false;
		}

		public override int GetHashCode() => new { X, Y }.GetHashCode();
	}
}