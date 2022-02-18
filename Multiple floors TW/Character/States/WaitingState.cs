using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : State
{
    [SerializeField] private float _waitingTime;

    public bool IsTimeToTransit { get; private set; }

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait() 
    {
        IsTimeToTransit = false;
        yield return new WaitForSeconds(_waitingTime);  
        StateEnabled?.Invoke();
        IsTimeToTransit = true;
    }
}