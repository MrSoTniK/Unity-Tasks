using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnAndMovement : MonoBehaviour
{
    [SerializeField] private float _projectileSpeed;
    [SerializeField] private GameObject _template;
    [SerializeField] private GameObject _firePoint;
    private GameObject _newProjectile;
    private bool _isPauseInShooting;

    private void Start()
    {      
        _isPauseInShooting = false;
    }

    void Update()
    {
        if (!_isPauseInShooting && Input.GetKey(KeyCode.F)) 
        {
            StartCoroutine(Shoot());
        }     
    }   

    IEnumerator Shoot() 
    {       
        _newProjectile = Instantiate(_template, _firePoint.transform.position, _firePoint.transform.rotation);
        Rigidbody projectileRigidBody = _newProjectile.GetComponent<Rigidbody>();
        projectileRigidBody.velocity = _projectileSpeed * _firePoint.transform.up;
        _isPauseInShooting = true;
        yield return new WaitForSeconds(1);
        _isPauseInShooting = false;
        StopCoroutine(Shoot());
    }
}