using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public float Health { get; private set; }
    public float MaxHealth => _maxHealth;

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void TakeDamage(float damage) 
    {
        Health -= damage;
        TryToDie();
    }

    public void TakeHeal(float healing) 
    {
        Health += healing;
    }

    private void TryToDie()
    {
        if (Health <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
        GameOverMenu.Load();
    }
}
