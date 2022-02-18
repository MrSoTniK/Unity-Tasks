using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected List<Entity> Targets;

    protected Entity CurrentTarget;
    protected int TargetID;

    public Entity Target => CurrentTarget;

    private void Awake()
    {
        TargetID = 0;
        if (Targets.Count > 0)
            CurrentTarget = Targets[0];
    }

    protected void NextTarget() 
    {
        TargetID++;
        if(Targets.Count > TargetID)
            CurrentTarget = Targets[TargetID];
    }
}