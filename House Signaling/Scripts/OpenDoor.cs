using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float _timeOfMove;
    [SerializeField] private float _degrees;
    private float _runningTime;   
    private bool isBreakIn;
    private Quaternion _positionOfMove;
    private Quaternion _startPosition;  

    private void Start()
    {
        isBreakIn = false;      
    }

    private void Update()
    {      
        if (isBreakIn == true) 
        {
            _runningTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(_startPosition, _positionOfMove, _runningTime / _timeOfMove);
        }     
    }

    private void OnTriggerEnter(Collider body)
    {      
         _positionOfMove = Quaternion.Euler(0, _degrees, 0);
         _startPosition = gameObject.transform.rotation;
         isBreakIn = true;    
    }    
}
