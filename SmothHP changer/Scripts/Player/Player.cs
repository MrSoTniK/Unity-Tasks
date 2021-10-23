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
        Health = Mathf.Clamp(health, 0, _maxHealth);
        HealthChanged?.Invoke(Health, _maxHealth);
        TryToDie();
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
