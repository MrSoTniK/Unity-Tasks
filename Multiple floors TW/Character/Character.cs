using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{
    [SerializeField] private State _targetChangerID;

    private void OnEnable()
    {
        _targetChangerID.StateEnabled.AddListener(NextTarget);
    }
}