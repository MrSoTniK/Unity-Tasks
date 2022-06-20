using UnityEngine;

[RequireComponent(typeof(FieldOfView))]
public class EnemyPatrolToPursueTransition : Transition
{
    private FieldOfView _fov;

    private void Awake()
    {
        _fov = GetComponent<FieldOfView>();
    }

    private void Update()
    {
        if (_fov.TryToFindPlayer())
            SetTransitionNecessity();
    }
}