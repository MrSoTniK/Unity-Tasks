using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float _jumpForce;
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing;
	[SerializeField] private LayerMask _whatIsGround;
	[SerializeField] private Transform _groundCheck;
	[SerializeField] private float _groundedRadius;

	[Header("Event")]
	[Space]
	public UnityEvent OnLandEvent;
	
	private bool _isGrounded;
	private Rigidbody2D _rigidBody;
	private bool _facingRight = true;
	private Vector2 _velocity = Vector2.zero;

	public void Move(float moveSpeed, bool isJump)
	{
		if (_isGrounded)
		{
			Vector2 targetVelocity = new Vector2(moveSpeed, _rigidBody.velocity.y);
			_rigidBody.velocity = Vector2.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _velocity, _movementSmoothing);

			if (moveSpeed > 0 && !_facingRight)
			{
				Flip();
			}
			else if (moveSpeed < 0 && _facingRight)
			{
				Flip();
			}

			if (isJump)
			{
				_isGrounded = false;
				_rigidBody.AddForce(new Vector2(0f, _jumpForce));
			}
		}
	}

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();	
	}

	private void FixedUpdate()
	{
		bool wasGrounded = _isGrounded;
		_isGrounded = false;

		Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.position, _groundedRadius, _whatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				_isGrounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}	

	private void Flip()
	{
		_facingRight = !_facingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}