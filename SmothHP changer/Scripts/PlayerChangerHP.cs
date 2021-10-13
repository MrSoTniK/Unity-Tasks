using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerChangerHP : MonoBehaviour
{
    [SerializeField] private ButtonChangerHP[] _buttons;
    [SerializeField] private float _speedOfChange;

    private Coroutine _coroutine;
    private Player _player;

    private void Start()
    {
        foreach(var button in _buttons)        
            button.ChangingHP += OnChangingHP;
        
        _player = GetComponent<Player>();
    }

    private void OnChangingHP(int damage)
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