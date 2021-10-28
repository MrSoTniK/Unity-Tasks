using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdTracker : MonoBehaviour
{
    [SerializeField] private Bird _bird;
    [SerializeField] private float _xOffest;

    private void Update()
    {
        transform.position = new Vector3(_bird.transform.position.x - _xOffest, transform.position.y, transform.position.z);
    }
}
