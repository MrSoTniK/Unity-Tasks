using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyMovement : MonoBehaviour
{
	[SerializeField] private Transform _player;
	[SerializeField] private float _viewDistance;
	[SerializeField] private float _fieldOfView;
	[SerializeField] private LayerMask _playerLayer;
	[SerializeField] private float _minDistanceBetweenWaypoints;
	[SerializeField] private float _minDistanceToPlayer;
	[SerializeField] private float _distanceAccuracy;
	[SerializeField] private LayerMask _obstaclesLayers; 

	private FieldOfView _fov;
	private List<Vector3> _waypoints;
	private NavMeshAgent _agent;
	private int _currentWaypointId;
	private bool _isPatrol = true;
	private bool _facingRight = true;
	private SpriteRenderer _renderer;

	public void GenerateWaypoints(List<Vector3> freeTiles, Vector3 startPosition) 
	{
		_waypoints = new List<Vector3>();
		_waypoints.Add(startPosition);
		List<Vector3> tilesForSecondWaypoint = new List<Vector3>();
		//Player player = FindObjectOfType<Player>();

		foreach (var tile in freeTiles) 		
		    if(tile != startPosition && Vector3.Distance(startPosition, tile) >= _minDistanceBetweenWaypoints && Vector3.Distance(_player.transform.position, tile) >= _minDistanceToPlayer)			
				tilesForSecondWaypoint.Add(tile);

		if (tilesForSecondWaypoint.Count > 0)
		{
			System.Random rand = new System.Random();
			int secondPointID = rand.Next(0, tilesForSecondWaypoint.Count);
			_waypoints.Add(tilesForSecondWaypoint[secondPointID]);
		}

		_agent = GetComponent<NavMeshAgent>();
		_agent.updateRotation = false;
		//	_agent.updatePosition = false;
		_agent.updateUpAxis = false;
	}

	public void DisablePatrol() 
	{
		_isPatrol = false;
		_fov.gameObject.SetActive(false);
		ChangeColor();
	}

	private void Start()
	{
		_currentWaypointId = 0;
		_fov = GetComponentInChildren<FieldOfView>();
		_renderer = GetComponent<SpriteRenderer>();
	}

	private void Update()
	{		
		_fov.SetAimDirection(_agent.velocity);
		CheckFacing();
		CheckDistanceToPLayer(_isPatrol);

		if (_isPatrol) 				
			Patrol();				
		else
			Pursue();
	}

	private void ChangeColor() 
	{
		_renderer.color = new Color(1f, 0f, 0f);
	}

	private void CheckDistanceToPLayer(bool isPatrol)
	{
		if (isPatrol) 
		{
			float distance = Vector3.Distance(transform.position, _player.position);
			if (distance < _viewDistance)
			{
				Vector3 dirToPlayer = (_player.position - transform.position).normalized;
				RaycastHit2D hit = Physics2D.Raycast(transform.position, dirToPlayer, _viewDistance, _obstaclesLayers);

				if (Vector3.Angle(_agent.velocity, dirToPlayer) < _fieldOfView / 2f && hit.collider == null)
				{
					_isPatrol = false;
					_fov.gameObject.SetActive(false);
					ChangeColor();
				}
			}
		}	
	}

	private void Pursue()
	{
		_agent.SetDestination(_player.position);
	}

	private void Patrol()
	{
		if (Vector2.Distance(transform.position, _waypoints[_currentWaypointId]) > _distanceAccuracy)		
			_agent.SetDestination(_waypoints[_currentWaypointId]);	

		if (Vector2.Distance(transform.position, _waypoints[_currentWaypointId]) <= _distanceAccuracy)		
			ChangeWaypoint();		
	}

	private void ChangeWaypoint()
	{
		if (_currentWaypointId < _waypoints.Count - 1)
			_currentWaypointId++;
		else
			_currentWaypointId = 0;
	}

	private void Flip()
	{
		_facingRight = !_facingRight;
		transform.localScale = new Vector3(transform.localScale.x * (-1), Mathf.Abs(transform.localScale.y), Mathf.Abs(transform.localScale.z));
		_fov.transform.localScale = new Vector3(_fov.transform.localScale.x * (-1), _fov.transform.localScale.y, _fov.transform.localScale.z);
	}

	private void CheckFacing() 
	{
		if (_agent.velocity.x > 0 && _agent.velocity.y == 0 && !_facingRight)	
			Flip();		
		else if (_agent.velocity.x < 0 && _agent.velocity.y == 0 && _facingRight)		
			Flip();		
	}
}
