using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDeletion : MonoBehaviour
{
    [SerializeField] private GameObject _enemy;

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
        Debug.Log(body.gameObject.GetType().ToString());
        if(body.gameObject.name.Contains(_enemy.name))
        Destroy(gameObject);
    }
}