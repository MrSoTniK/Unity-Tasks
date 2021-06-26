using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : ObjectPool
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _pauseBetweenShots;

    private float elapsedTime = 0;

    private void Start()
    {
        Initialize(_projectilePrefab);     
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= _pauseBetweenShots) 
        {
            elapsedTime = _pauseBetweenShots;

            if (Input.GetKeyDown(KeyCode.F) && TryGetObject(out GameObject projectile)) 
            {
                elapsedTime = 0;
                Shoot(projectile);
            }
        }      
    }

    private void Shoot(GameObject projectile)
    {
        projectile.SetActive(true);
        projectile.transform.position = transform.position;
        Rigidbody projectileRigidBody = projectile.GetComponent<Rigidbody>();
        projectileRigidBody.velocity = _projectileSpeed * transform.up;       
    }
}
