using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private string[] _animationsToSetActive;
    [SerializeField] private string[] _animationsToSetInactive;
    [SerializeField] private float _pauseTime;
    [SerializeField] private float _speed;

    public float Speed { get; private set; }
    public float PauseTime { get; private set; }
    public string[] AnimationsToSetActive { get; private set; }
    public string[] AnimationsToSetInactive { get; private set; }

    public void Start()
    {
        Speed = _speed;
        PauseTime = _pauseTime;
        AnimationsToSetActive = _animationsToSetActive;
        AnimationsToSetInactive = _animationsToSetInactive;
    }    
}
