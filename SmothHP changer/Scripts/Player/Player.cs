using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private float _maxHealth;

    public float Health { get; private set; }
    public float MaxHealth => _maxHealth;

    public event UnityAction<float, float> HealthChanged;

    private void Awake()
    {
        Health = _maxHealth;
    }

    public void ChangeHealth(float health) 
    {       
        Health = CheckHealth(health);
        HealthChanged?.Invoke(Health, _maxHealth);
        TryToDie();
    }

    public void TryToDie()
    {
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
        GameOverMenu.Load();
    }

    private float CheckHealth(float health) 
    {
        float newHealth = health;

        if (health > _maxHealth)
            newHealth = _maxHealth;

        if (health <= 0)
            newHealth = 0;

        return newHealth;
    }    
}