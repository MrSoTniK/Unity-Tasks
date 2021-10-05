using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speedOffset;
    [SerializeField] private Vector3 _positionOffest;
    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;
    [SerializeField] private float _bottomLimit;
    [SerializeField] private float _topLimit;

    private Vector3 _velocity;

    private void Start()
    {
        _velocity = Vector3.zero;
    }

    private void Update()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = _player.transform.position;

        endPosition.x += _positionOffest.x;
        endPosition.y += _positionOffest.y;
        endPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(startPosition, endPosition, ref _velocity, _speedOffset * Time.deltaTime);

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit),
            Mathf.Clamp(transform.position.y, _bottomLimit, _topLimit),
            transform.position.z
        );
    }
}
