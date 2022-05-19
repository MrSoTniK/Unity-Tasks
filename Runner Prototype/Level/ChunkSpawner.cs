using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ChunkSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _chunkPrefab;
    [SerializeField] private float _spawnDistanceZ;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _maxChunksQuantity;
    [SerializeField] private GameObject[] _startChunks;

    private Vector3 _currentPosition;
    private Vector3 _spawnDistance;
    private GameObject _currentChunk;

    public UnityAction ChunksSpawned;

    private void Awake()
    {
        _currentPosition = _startPosition;        
        _spawnDistance = new Vector3(0, 0, _spawnDistanceZ);              
        _currentChunk = _startChunks[0];     

        _currentChunk.TryGetComponent<Chunk>(out Chunk chunk);
        chunk.ColliderTouched += Spawn;
    }

    private void Spawn()
    {
        _currentChunk.TryGetComponent<Chunk>(out Chunk chunk);
        chunk.ColliderTouched -= Spawn;       

        List<GameObject> chunkObjects = new List<GameObject>();
        for (int i = 0; i < _maxChunksQuantity; i++) 
        {
            chunkObjects.Add(SpawnChunk(_currentPosition));
            _currentPosition += _spawnDistance;
        }       
            
        _currentChunk = chunkObjects[0];
        _currentChunk.TryGetComponent<Chunk>(out Chunk arrayChunk);
        arrayChunk.ColliderTouched += Spawn;
        chunkObjects.Clear();
        ChunksSpawned?.Invoke();
    }

    private GameObject SpawnChunk(Vector3 position) 
    {
        GameObject chunkObject = Instantiate(_chunkPrefab.gameObject, position, Quaternion.identity, transform);      
        return chunkObject;
    }
}