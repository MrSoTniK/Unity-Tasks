using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _velocity;
    [SerializeField] private PlayerController _controller;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing;

    private Rigidbody _rigidBody;
    private Vector3 _currentVelocity;

    private void Awake()
    {      
        _rigidBody = GetComponent<Rigidbody>();
        _currentVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {       
        Vector3 targetVelocity = new Vector3(_controller.HorizontalDirection * _velocity * Time.fixedDeltaTime, 0, _controller.VerticalDirection * _velocity * Time.fixedDeltaTime);
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _currentVelocity, _movementSmoothing);

        if (targetVelocity == Vector3.zero)
            this.enabled = false;
    }
}