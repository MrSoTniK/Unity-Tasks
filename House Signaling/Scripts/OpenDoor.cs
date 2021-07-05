using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float _pauseBetweenMoves;
    [SerializeField] private float _degrees;
    [SerializeField] private float _degreesRotationStep; 

    private void OnTriggerEnter(Collider body)
    {             
        StartCoroutine(Open());
    }    

    private IEnumerator Open() 
    {
       float currentDegreesQuantity = 0;

       while(currentDegreesQuantity != _degrees) 
       {
            currentDegreesQuantity += _degreesRotationStep;
            transform.rotation = Quaternion.Euler(0, currentDegreesQuantity, 0);
            yield return new WaitForSeconds(_pauseBetweenMoves);
        }            
    }
}
