using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class CardPlacer : MonoBehaviour
{
    [SerializeField] protected int Health;
    [SerializeField] protected int Damage;

    public void TakeDamage(int damage) 
    {
        Health -= damage;
    }

    public void TakeHeal(int healing) 
    {
        Health += healing;
    }

    public void Attack(CardPlacer target) 
    {
        target.TakeDamage(Damage);
    }

    abstract public void ShowInformation();    
}