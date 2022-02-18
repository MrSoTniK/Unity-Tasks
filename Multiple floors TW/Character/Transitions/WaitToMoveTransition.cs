using UnityEngine;

[RequireComponent(typeof(WaitingState))]
public class WaitToMoveTransition : Transition
{
    [SerializeField] private WaitingState _waitingState;

    private void Update()
    {
        if (_waitingState.IsTimeToTransit)
            SetTransitionNecessity();
    }
}