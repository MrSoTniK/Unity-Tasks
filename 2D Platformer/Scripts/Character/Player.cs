using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using IJunior.TypedScenes;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private Slider _hpBar;
    [SerializeField] private TMP_Text _hpIndicator;

    public UnityEvent OnPlayerDeath;
    private int _currentHealth;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        SetParametersOfUI();

        if (_currentHealth <= 0)
            Die();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        SetParametersOfUI();
    }

    private void SetParametersOfUI() 
    {
        _hpIndicator.text = _currentHealth.ToString();
        _hpBar.value = (float)_currentHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        OnPlayerDeath.Invoke();
        GameOver.Load();
    }
}
