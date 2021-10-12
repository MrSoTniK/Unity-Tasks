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

    public UnityAction OnPlayerDeath;
    private float _currentHealth;
    private Coroutine _changingHealthCoroutine;

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        SetParametersOfUI();

        CheckHealth();
    }

    public void StartChangingHealth(int damage, int speed) 
    {
        if (_changingHealthCoroutine != null)
            StopCoroutine(_changingHealthCoroutine);

        _changingHealthCoroutine = StartCoroutine(ChangeHealth(damage, speed));
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        SetParametersOfUI();
    }

    private void SetParametersOfUI() 
    {
        _hpIndicator.text = ((int) _currentHealth).ToString();
        _hpBar.value = (float)_currentHealth;
    }

    private void Die()
    {
        gameObject.SetActive(false);
        OnPlayerDeath.Invoke();
        GameOver.Load();
    }

    private void CheckHealth() 
    {
        if (_currentHealth <= 0)
            Die();

        if (_currentHealth > _maxHealth)
            _currentHealth = _maxHealth;
    }

    private IEnumerator ChangeHealth(float damage, float speed) 
    {       
        float targetHealth = _currentHealth - damage;
        while (_currentHealth != targetHealth && _currentHealth <= _maxHealth) 
        {
            _currentHealth = Mathf.MoveTowards(_currentHealth, targetHealth, speed * Time.deltaTime);
            CheckHealth();
            SetParametersOfUI();            
            yield return new WaitForEndOfFrame();
        }     
    }
}
