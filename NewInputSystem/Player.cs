using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _takeDistance;
    [SerializeField] private float _holdDistance;
    [SerializeField] private float _throwForce;

    private PlayerInput _input;
    private Vector2 _direction;
    private Vector2 _rotate;
    private Vector2 _rotation;
    private GameObject _currentGameObject;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
        _input.Player.PcikUp.performed += ctx => TryPickUp();
        _input.Player.Throw.performed += ctx => Throw();
        _input.Player.Drop.performed += ctx => Throw(true);
        _input.Player.Click.performed += ctx =>
        {
            if (ctx.interaction is MultiTapInteraction)
                Shoot();
        };
    }

    private void Update()
    {
        _rotate = _input.Player.Look.ReadValue<Vector2>();
        _direction = _input.Player.Move.ReadValue<Vector2>();

        Look(_rotate);
        Move(_direction);
    }

    private void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude < 0.1)
            return;

        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaledMoveSpeed;
    }

    private void Look(Vector2 rotate)
    {
        if (rotate.sqrMagnitude < 0.1)
            return;

        float scaledRotateSpeed = _rotateSpeed * Time.deltaTime;
        _rotation.y += rotate.x * scaledRotateSpeed;
        _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -90, 90);
        transform.localEulerAngles = _rotation;
    }

    private void TryPickUp() 
    {
        if(Physics.Raycast(transform.position, transform.forward, out var hitInfo, _takeDistance) && !hitInfo.collider.gameObject.isStatic)
        {
            _currentGameObject = hitInfo.collider.gameObject;
            _currentGameObject.transform.position = default;
            _currentGameObject.transform.SetParent(transform, worldPositionStays: false);
            _currentGameObject.transform.localPosition += new Vector3(0, 0, _holdDistance);
            _currentGameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void Throw(bool drop = false) 
    {
        _currentGameObject.transform.parent = null;

        var rigidBody = _currentGameObject.GetComponent<Rigidbody>();
        rigidBody.isKinematic = false;

        if (!drop)
            rigidBody.AddForce(transform.forward * _throwForce, ForceMode.Impulse);
    }

    private void Shoot() 
    {
        Debug.Log("Shoot");
    }
}
