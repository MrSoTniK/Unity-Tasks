using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class HealthField : MonoBehaviour
{
    [SerializeField] private Card _card;
    private TMP_Text _health;

    private void OnEnable()
    {
        _health = GetComponent<TMP_Text>();
        _health.text = _card.HP.ToString();
        _card.HealthChanged += OnHealthChanged;       
    }

    private void OnDisable()
    {
        _card.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health) 
    {
        _health.text = health.ToString();
    }
}