using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BorderLogic : MonoBehaviour
{  
    private void OnTriggerEnter(Collider body)
    {
        if (body.TryGetComponent<Projectile>(out Projectile projectile))
            projectile.Die();
    }
}
