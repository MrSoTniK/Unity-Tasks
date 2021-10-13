using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IJunior.TypedScenes;

public class Player : MonoBehaviour
{
    [SerializeField] public float MaxHealth;
    public float Health { get; private set; }

    private void Start()
    {
        Health = MaxHealth;
    }

    public void ChangeHealth(float health) 
    {
        Health = health;
    }

    public void CheckHealth() 
    {
        if (Health > MaxHealth)
            Health = MaxHealth;

        if(Health <= 0) 
        {
            Health = 0;
            Die();
        }          
    }

    public void Die() 
    {
        gameObject.SetActive(false);
        GameOverMenu.Load();
    }
}
