using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(CardRandomizer))]
public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemiesPrefabs;
    [SerializeField] private GameObject[] _itemsPrefabs;
    [SerializeField] private Player _player;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _layers;

    private CardRandomizer _randomizer;
    public int EnemiesCount => _enemiesPrefabs.Count();
    public int ItemsCount => _itemsPrefabs.Count();

    private void OnEnable()
    {
        _player.PlayerMoved += SpawnCard;
    }

    private void OnDisable()
    {
        _player.PlayerMoved -= SpawnCard;
    }

    private void Start()
    {
        _randomizer = GetComponent<CardRandomizer>();
        foreach (var point in _spawnPoints) 
        {
            if (point.position.x != 0 || point.position.y != 0)
                InstantiateCard(point.position);
        }
    }

    private void ChooseAndSpawn(Vector2 position) 
    {
        List<Vector2> emptyPoints = new List<Vector2>();
        foreach(var point in _spawnPoints) 
        {
            if (!CheckCardExistence(point))
                emptyPoints.Add(point.position);
        }

        switch (emptyPoints.Count) 
        {
            case 1:
                SpawnOneCard(emptyPoints[0]);
                break;
        }
    }

    private void SpawnCard(Vector2 position, Vector2 direction) 
    {
        if (direction.y > 0)
            Spawn(position);
        else
            ChooseAndSpawn(position);
    }

    private bool CheckCardExistence(Transform point) 
    {
        RaycastHit2D hit = Physics2D.Raycast(point.position, Vector2.zero, _distance, _layers);
        if (hit.collider != null)
            return true;
        else
            return false;
    }

    private void SpawnOneCard(Vector2 emptyPoint) 
    {      
        var points = _spawnPoints.Where(point => point.position.x == emptyPoint.x).ToList();
        var sortedPoins = points.OrderBy(coord => coord.position.y).ToList();
        int index = -1;
        for(int i = 0; i < sortedPoins.Count; i++) 
        {
            if (sortedPoins[i].position.y == emptyPoint.y) 
            {
                index = i;
                break;
            }             
        }
      
        if (index == sortedPoins.Count - 1)
            Spawn(emptyPoint);
        else        
            SpawnWithCardsMovement(sortedPoins, emptyPoint);          
    }

    private void Spawn(Vector3 position) 
    {
        StartCoroutine(StartDelay(position));
    }

    private IEnumerator StartDelay(Vector3 position) 
    {
        yield return new WaitForSeconds(_spawnDelay);
        InstantiateCard(position);
    }

    private void InstantiateCard(Vector3 position) 
    {
        bool isEnemy;
        int id = _randomizer.Randomize(out isEnemy);
        GameObject currentPrefab;
        switch (isEnemy) 
        {
            case true:
                currentPrefab = Instantiate(_enemiesPrefabs[id], position, Quaternion.identity);
                break;
            case false:
                currentPrefab = Instantiate(_itemsPrefabs[id], position, Quaternion.identity);
                break;
        }
        currentPrefab.transform.SetParent(transform);
    }

    private void SpawnWithCardsMovement(List<Transform> points, Vector2 emptyPoint) 
    {
        List<RaycastHit2D> hits = new List<RaycastHit2D>();
        List<Vector2> hitsPositions = new List<Vector2>();

        foreach (var point in points)
        {
            RaycastHit2D hit = Physics2D.Raycast(point.position, Vector2.zero, _distance, _layers);
            if (hit.collider != null && hit.transform.position.y > emptyPoint.y) 
            {
                hits.Add(hit);
                hitsPositions.Add(hit.collider.transform.position);
            }
        }

        Vector3 previousPosition = hitsPositions[hitsPositions.Count - 1];

        for (int i = 0; i< hits.Count; i++) 
        {
            if (hits[i].collider.TryGetComponent<Card>(out Card card)) 
            {
                if (i == 0)               
                    card.Move(emptyPoint);                
                else                
                    card.Move(hitsPositions[i - 1]);               
            }               
        }

        Spawn(previousPosition);
    }
}