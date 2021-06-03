using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedOfRotation;
    [SerializeField] private float _stepDistance;
    [SerializeField] private float _stepOfRotation;

    private void Update()
    { 
         MoveLeft();
         MoveRight();
         MoveUp();
         MoveDown();
         TurnPlayerLeft();
         TurnPlayerRight();
    }   

    private void MovePlayer(float x, float z)
    {
        Vector3 move = new Vector3(x, 0, z);
        Vector3 targetPosition = transform.position + move;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
    }

    private void TurnPlayer(float direction) 
    {     
        Quaternion rotation = Quaternion.AngleAxis(direction*_stepOfRotation, Vector3.forward);
        Quaternion targetRotation = transform.rotation * rotation;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _speedOfRotation * Time.deltaTime);
    }

    private void TurnPlayerLeft() 
    {
        if (Input.GetKey(KeyCode.Q) && !Input.GetKey(KeyCode.E)) 
        {
            TurnPlayer(1);
        }
    }

    private void TurnPlayerRight()
    {
        if (Input.GetKey(KeyCode.E) && !Input.GetKey(KeyCode.Q))
        {
            TurnPlayer(-1);
        }
    }

    private void MoveLeft()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayer(-_stepDistance, 0);         
        }
    }

    private void MoveRight()
    {
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayer(_stepDistance, 0);      
        }
    }

    private void MoveUp()
    {
        if (Input.GetKey(KeyCode.W))
        {
            MovePlayer(0, _stepDistance);         
        }
    }

    private void MoveDown() 
    {
        if (Input.GetKey(KeyCode.S))
        {
            MovePlayer(0, -_stepDistance);          
        }
    }
}
