using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitChecker : MonoBehaviour
{     
    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.TakeDamage();
    }
}
