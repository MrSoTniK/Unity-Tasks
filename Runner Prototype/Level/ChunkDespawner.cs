using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(ChunkSpawner))]
public class ChunkDespawner : MonoBehaviour
{   
    [SerializeField] private int _maxChunksQuantity;

    private List<Zone> _chunks;
    private ChunkSpawner _spawner;
    private int _chunksQuantityForDelete;   

    private void Awake()
    {
        _spawner = GetComponent<ChunkSpawner>();
        _chunksQuantityForDelete = _maxChunksQuantity / 2;
    }

    private void Start()
    {
        _chunks = new List<Zone>();
    }

    private void OnEnable()
    {
        _spawner.ChunksSpawned += TryToDespawnTrash;
    }

    private void OnDisable()
    {
        _spawner.ChunksSpawned -= TryToDespawnTrash;
    }

    private void TryToDespawnTrash() 
    {
        _chunks = GetComponentsInChildren<Zone>().ToList<Zone>();
        if (_chunks.Count > _maxChunksQuantity) 
        {
            for (int i = 0; i < _chunksQuantityForDelete; i++) 
            {
                Destroy(_chunks[0].gameObject);
                _chunks.RemoveAt(0);
            }              
        }                   
    }
}