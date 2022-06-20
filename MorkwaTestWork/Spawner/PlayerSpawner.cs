using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerPointsGenerator _generator;
    [SerializeField] private Vector3 _offset;

    private void OnEnable()
    {
        _generator.PointsWereGenerated += SpawnPlayer;
    }

    private void OnDisable()
    {
        _generator.PointsWereGenerated -= SpawnPlayer;
    }

    private void SpawnPlayer() 
    {
        _player.transform.position = _generator.SpawnPoint + _offset;
        _player.gameObject.SetActive(true);
    }
}
