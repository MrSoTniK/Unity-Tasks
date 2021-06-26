using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider body)
    {
        if (body.TryGetComponent<Player>(out Player player))
            player.Die();
        if (body.TryGetComponent<Projectile>(out Projectile projectile)) 
        {
            Die();
            projectile.Die();
        }           
    }

    public void Die() 
    {
        gameObject.SetActive(false);
    }
}
