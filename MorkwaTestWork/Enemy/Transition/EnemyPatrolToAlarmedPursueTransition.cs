using UnityEngine;

public class EnemyPatrolToAlarmedPursueTransition : Transition
{
    [SerializeField] protected NoiseController Controller;
  
    protected void Update()
    {
        if (Controller.ReceivedNoiseValue >= Controller.MaxNoiseValue)
            SetTransitionNecessity();
    }
}