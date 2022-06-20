using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Entity Target { get; private set; }

    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }

    public void Init(Entity target)
    {
        Target = target;
    }

    protected void SetTransitionNecessity()
    {
        NeedTransit = true;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}