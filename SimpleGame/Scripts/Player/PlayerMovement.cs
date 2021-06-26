using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _speedOfRotation;
    [SerializeField] private float _stepDistance;
    [SerializeField] private float _stepOfRotation;

    private List<KeyCode> _keys = new List<KeyCode>();

    private void Start()
    {
        SetControlButtons();
    }

    private void Update()
    {     
        TryMakeMove();
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

    private void TryMakeMove() 
    {
        float xValue = 0, zValue = 0;
        float direction = 0;
        bool isAnyKeyPressed = false;

        List<KeyCode> pressedKeys = _keys.Where(key => Input.GetKey(key) == true).ToList();

        if(pressedKeys.Count > 0) 
        {
            isAnyKeyPressed = true;
        }

        if (isAnyKeyPressed) 
        {
            foreach (var pressedKey in pressedKeys)          
                DetermineValuesForPressedKey(pressedKey, xValue, zValue, direction, out xValue, out zValue, out direction);          

            if(xValue != 0 || zValue != 0)
                MovePlayer(xValue, zValue);

            if(direction != 0)
                TurnPlayer(direction);
        }                    
    }

    private void SetControlButtons() 
    {
        _keys.Add(KeyCode.A);
        _keys.Add(KeyCode.D);
        _keys.Add(KeyCode.W);
        _keys.Add(KeyCode.S);
        _keys.Add(KeyCode.Q);
        _keys.Add(KeyCode.E);
    }

    private void DetermineValuesForPressedKey(KeyCode key, float initialX, float initialZ, float initialDirection, out float finalX, out float finalZ, out float finalDirection) 
    {
        finalX = initialX;
        finalZ = initialZ;
        finalDirection = initialDirection;

        switch (key) 
        {
            case KeyCode.A:
                finalX -= _stepDistance;
                break;
            case KeyCode.D:
                finalX += _stepDistance;
                break;
            case KeyCode.W:
                finalZ += _stepDistance;
                break;
            case KeyCode.S:
                finalZ -= _stepDistance;
                break;
            case KeyCode.Q:
                finalDirection += 1;
                break;
            case KeyCode.E:
                finalDirection += -1;
                break;
        }
    }
}
