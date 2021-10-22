using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShotgunBullet : Bullet
{
    private Vector2 _direction;

    private void Start()
    {
        float dirX = Vector2.left.x;
        float dirY = (float)(Math.Round(Mathf.Cos(Mathf.PI / 2 + transform.rotation.z), 1));
        _direction = new Vector2(dirX, dirY);
    }

    protected override void Move()
    {     
        transform.Translate(_direction * Speed * Time.deltaTime, Space.World);
    }

    private void Update()
    {
        Move();
    }
}
