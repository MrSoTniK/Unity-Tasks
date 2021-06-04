using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderLogic : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;

    private void OnTriggerEnter(Collider body)
    {
        if(body.gameObject.name.Contains(_projectile.name))
        Destroy(body.gameObject);
    }
}