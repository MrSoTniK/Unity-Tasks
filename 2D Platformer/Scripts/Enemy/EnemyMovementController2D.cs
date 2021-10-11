using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Enemy))]
public class EnemyMovementController2D : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _groundedRadius;
    [SerializeField] private float _raycastDistance;
    [Range(0, .3f)] [SerializeField] private float _movementSmoothing;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _environmentLayers;

    private Rigidbody2D _rigidBody;
    private bool _isFacingRight;
    private bool _isGrounded;
    private Vector2 _velocity = Vector2.zero;
    private float _currentDirection;  

    public void MoveToDestination(bool isPatrolling, int previousId, float destinationPositionX, int length, out int currentId) 
    {
        currentId = previousId;
        float newDirection = _currentDirection;

        if (transform.position.x < destinationPositionX)
            newDirection = 1;

        if (transform.position.x > destinationPositionX)
            newDirection = -1;

        if (_currentDirection != newDirection && _currentDirection != 0 && isPatrolling)       
            currentId = ChangePoint(currentId, length);          
        
        _currentDirection = newDirection;

        Move(_currentDirection * _moveSpeed, false);
    }

    public void OnPlayerLoosing()
    {
        _currentDirection = 0;
    }

    private void Move(float moveSpeed, bool isJump)
    {
        if (_isGrounded)
        {
            Vector2 targetVelocity = new Vector2(moveSpeed, _rigidBody.velocity.y);
            _rigidBody.velocity = Vector2.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _velocity, _movementSmoothing);
        }

        if (moveSpeed > 0 && !_isFacingRight)
            Flip();
        else if (moveSpeed < 0 && _isFacingRight)
            Flip();

        if (isJump)
            _rigidBody.AddForce(new Vector2(0f, moveSpeed));
    }

    private void Start()
    {
        _isGrounded = true;
        _rigidBody = GetComponent<Rigidbody2D>();
        _isFacingRight = true;
        _currentDirection = 0;
    }  

    private void Update()
    {
        CheckGround();
        CheckWall();
    }  

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector2 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private int ChangePoint(int previousId, int length) 
    {
        int waypointLastId = length - 1;
        int currentId = previousId + 1;
        if (currentId > waypointLastId)
            currentId = 0;
        return currentId;
    }  

    private void CheckWall() 
    {
        if (_isGrounded) 
        {
            Vector2 direction = new Vector2(_currentDirection, 0);         
            RaycastHit2D[] hits = Physics2D.RaycastAll(_wallChecker.position, direction, _raycastDistance, _environmentLayers);
            Debug.DrawRay(_wallChecker.position, direction, Color.red, _raycastDistance);
            if(hits.Length != 0) 
            {
                _isGrounded = false;
                Move(_jumpForce, true);
            }           
        }      
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundChecker.position, _groundedRadius, _environmentLayers);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)           
                _isGrounded = true;                         
            else
                _isGrounded = false;
        }
    }
}
