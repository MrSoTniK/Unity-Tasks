using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _takenDamage;

    private int _currentHealth;

    public void TakeDamage() 
    {
        _currentHealth -= _takenDamage;

        if (_currentHealth <= 0)
            Die();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    private void Die() 
    {
        Destroy(gameObject);
    }
}
