using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] protected float Speed;

    protected abstract void Move();

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy)) 
        {
            enemy.TakeDamage(Damage);
            Destroy(gameObject);
        }
    }
}