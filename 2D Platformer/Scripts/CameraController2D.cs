using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController2D : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _speedOffset;
    [SerializeField] private Vector2 _positionOffest;
    [SerializeField] private Vector2 _thirdQuarterLimit;
    [SerializeField] private Vector2 _firstQuarterLimit;

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
            Mathf.Clamp(transform.position.x, _thirdQuarterLimit.x, _firstQuarterLimit.x),
            Mathf.Clamp(transform.position.y, _thirdQuarterLimit.y, _firstQuarterLimit.y),
            transform.position.z
        );
    }
}
