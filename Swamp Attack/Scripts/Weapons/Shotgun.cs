using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private float _distanceBetweenBullets;
    [SerializeField] private float _flyDegree;

    public override void Shoot(Transform shootPoint)
    {
        Vector3 upperShootPoint = new Vector3(shootPoint.position.x, shootPoint.position.y + _distanceBetweenBullets, shootPoint.position.z);
        Vector3 lowerShootPoint = new Vector3(shootPoint.position.x, shootPoint.position.y - _distanceBetweenBullets, shootPoint.position.z);
        Instantiate(Bullet, shootPoint.position, Quaternion.identity);
        Instantiate(Bullet, upperShootPoint, Quaternion.Euler(0, 0, -_flyDegree));
        Instantiate(Bullet, lowerShootPoint, Quaternion.Euler(0, 0, _flyDegree));
    }
}
