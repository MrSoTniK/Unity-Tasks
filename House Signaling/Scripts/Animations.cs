using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private string[] _animationsToSetActive;
    [SerializeField] private string[] _animationsToSetInactive;
    [SerializeField] private int _waypointId;

    public string[] AnimationsToSetActive { get; private set; }
    public string[] AnimationsToSetInactive { get; private set; }

    public int WaypointId { get; private set; }

    void Start()
    {
        AnimationsToSetActive = _animationsToSetActive;
        AnimationsToSetInactive = _animationsToSetInactive;
        WaypointId = _waypointId;
    }
}
