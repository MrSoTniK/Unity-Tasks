using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMoveDefiner : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing;

	private Rigidbody2D _rigidBody;
	private bool _facingRight;
	private Vector2 _velocity;

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
		_facingRight = true;
		_velocity = Vector2.zero;
	}

	public void Move(float moveSpeedX, float moveSpeedY)
	{
		Vector2 targetVelocity = new Vector2(moveSpeedX, moveSpeedY);
		_rigidBody.velocity = Vector2.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _velocity, _movementSmoothing);

		if (moveSpeedX > 0 && !_facingRight)		
			Flip();
		
		else if (moveSpeedX < 0 && _facingRight)	
			Flip();	
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		Vector2 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}