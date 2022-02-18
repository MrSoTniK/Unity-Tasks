using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRandomizer : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private int _randomnicityCoefficient;
    [SerializeField] private CardSpawner _spawner;
    [SerializeField] private int _chanceRiseCoefficient;
 
    private Player _player;
    private System.Random _random;
    private int _enemyChanceRise;
    private int _itemChanceRise;

    private void Awake()
    {
        _enemyChanceRise = 0;
        _itemChanceRise = 0;
        _random = new System.Random();
    }

    private void Start()
    {
        _player = GetComponentInChildren<Player>();
    }

    public int Randomize(out bool isEnemy) 
    {
        int listId;
        int enemyChance = _random.Next(0, _randomnicityCoefficient + _enemyChanceRise);
        int itemChance = _random.Next(0, _randomnicityCoefficient + _itemChanceRise);
        if(enemyChance >= itemChance) 
        {
            listId = _random.Next(0, _spawner.EnemiesCount);
            _itemChanceRise += _chanceRiseCoefficient;
            _enemyChanceRise = 0;
            isEnemy = true;
        }
        else 
        {
            listId = _random.Next(0, _spawner.ItemsCount);
            _enemyChanceRise += _itemChanceRise;
            _itemChanceRise = 0;
            isEnemy = false;
        }           

        return listId;
    }
}
