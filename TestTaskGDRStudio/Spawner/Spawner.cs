using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _spawnRadius;
    [SerializeField] private float _quantity;
    [SerializeField] private Vector2 _center;
    [SerializeField] private GameObject _prefab;

    private float _angleStep;
    public float Quantity => _quantity;

    private void Start()
    {
        Time.timeScale = 1f;
        _angleStep = 360 / _quantity;
        SpawnAlongCircle();
    }

    private void SpawnAlongCircle() 
    {
        float angle = 0;
        for(int i = 1; i <= _quantity; i++) 
        {
            angle += _angleStep;
            Vector2 position = new Vector2(_center.x + _spawnRadius * Mathf.Cos(angle * Mathf.Deg2Rad), _center.y + _spawnRadius * Mathf.Sin(angle * Mathf.Deg2Rad));
            Instantiate(_prefab, position, Quaternion.identity).transform.SetParent(transform);
        }
    }
}