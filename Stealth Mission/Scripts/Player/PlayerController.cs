using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float _movementSmoothing;

	private Rigidbody2D _rigidBody;
	private bool _facingRight = true;
	private Vector2 _velocity = Vector2.zero;

	public void Move(float targetVelocityX, float targetVelocityY)
	{
		Vector3 targetVelocity = new Vector3(targetVelocityX, targetVelocityY, 0);

		_rigidBody.velocity = Vector2.SmoothDamp(_rigidBody.velocity, targetVelocity, ref _velocity, _movementSmoothing);

		if (targetVelocityX > 0 && !_facingRight)
		{
			Flip();
		}
		else if (targetVelocityX < 0 && _facingRight)
		{
			Flip();
		}
	}

	private void Start()
	{
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		transform.localScale = new Vector3(transform.localScale.x * (-1), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
	}
}
