using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HealthChanger : MonoBehaviour
{
    [SerializeField] private Button _damagerButton;
    [SerializeField] private Button _healerButton;
    [SerializeField] private int _damage;
    [SerializeField] private Player _player;
    [SerializeField] private float _speedOfChange;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _damagerButton.onClick.AddListener(delegate { OnButtonClick(_damage);} );
        _healerButton.onClick.AddListener(delegate { OnButtonClick(-_damage); });
    }

    private void OnDisable()
    {
        _damagerButton.onClick.RemoveListener(delegate { OnButtonClick(_damage); });
        _healerButton.onClick.RemoveListener(delegate { OnButtonClick(-_damage); });
    }

    private void OnButtonClick(int damage)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeHP(damage));
    }

    private IEnumerator ChangeHP(int damage) 
    {       
        float currentHealth = _player.Health;
        float targetHealth = currentHealth - damage;

        while (currentHealth != targetHealth && currentHealth <= _player.MaxHealth)
        {
            currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, _speedOfChange * Time.deltaTime);
            _player.ChangeHealth(currentHealth);
            yield return new WaitForEndOfFrame();
        }
    }
}