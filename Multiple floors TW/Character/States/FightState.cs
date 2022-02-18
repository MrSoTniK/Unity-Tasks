using System.Collections.Generic;
using UnityEngine;

public class FightState : State
{
    [SerializeField] private float _raycastRadius;
    [SerializeField] private List<Enemy> _enemies;

    public bool isFightOver { get; private set; }

    private void OnEnable()
    {
        isFightOver = false;
        if (!CheckEnemies())
            isFightOver = true;
    }

    private bool CheckEnemies() 
    {
        bool areEnemiesHere = false;

        return areEnemiesHere;
    }
}
