using UnityEngine;
using UnityEngine.Events;

public abstract class Entity : MonoBehaviour
{
    [SerializeField] protected int MaxHealth;

    protected Entity CurrentTarget;
    protected int CurrentHealth;

    public int HP => CurrentHealth;
    public Entity Target => CurrentTarget;

    public UnityAction EntityDied;
    public UnityAction<int> HealthChanged;

    protected virtual void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        int health = CurrentHealth - damage;
        health = Mathf.Clamp(health, 0, MaxHealth);
        CurrentHealth = health;
        HealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Heal(int healing)
    {
        int health = CurrentHealth + healing;
        health = Mathf.Clamp(health, 0, MaxHealth);
        CurrentHealth = healing;
        HealthChanged?.Invoke(CurrentHealth);
    }

    protected virtual void Die()
    {
        EntityDied?.Invoke();
        Destroy(gameObject);
    }
}