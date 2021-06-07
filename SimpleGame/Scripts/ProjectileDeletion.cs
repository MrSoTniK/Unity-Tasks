using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeletion : MonoBehaviour
{
    private float _runningTime;

    private void Start()
    {
        _runningTime = 0;
    }

    private void Update()
    {
        _runningTime += Time.deltaTime;
        if(_runningTime > 3f) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider body)
    {
        if(body.TryGetComponent<EnemyLogic>(out EnemyLogic enemy))
        Destroy(gameObject);
    }
}