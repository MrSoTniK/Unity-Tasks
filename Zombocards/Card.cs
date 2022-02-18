using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected float Speed;
    [SerializeField] [Range(0, 100)] private int _cardId;

    public int HP => Health;
    public int Id => _cardId;
    public UnityAction<int> HealthChanged;

    public void TakeDamage(int damage)
    {
        int damageHealth = Health - damage;
        Health = Mathf.Clamp(damageHealth, 0, Health);
        HealthChanged?.Invoke(Health);
    }

    public void TakeHeal(int healing, int maxHP)
    {
        int healedHealth = Health + healing;
        Health = Mathf.Clamp(healedHealth, 0, maxHP);
        HealthChanged?.Invoke(Health);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void ShowInformation()
    {

    }

    public void Move(Vector3 targetPosition)
    {
        StartCoroutine(MakeMove(targetPosition));
    }  

    protected virtual IEnumerator MakeMove(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}