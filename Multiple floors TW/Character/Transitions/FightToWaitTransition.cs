using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FightState))]
public class FightToWaitTransition : Transition
{
    [SerializeField] private FightState _fightState;

    private void Update()
    {
        if (_fightState.isFightOver)
            SetTransitionNecessity();
    }
}