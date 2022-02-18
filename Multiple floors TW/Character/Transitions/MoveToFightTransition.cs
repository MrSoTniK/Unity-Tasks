using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToFightTransition : Transition
{
    [SerializeField] private float _inaccuracy;

    private Entity _target;

    protected override void OnEnable()
    {
        base.OnEnable();
        _target = GetComponent<Character>().Target;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _target.transform.position) <= _inaccuracy)
            NeedTransit = true;
    }
}