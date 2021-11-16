using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _field;
    [SerializeField] private Player _player;
    [SerializeField] private HealthChangerButton[] _buttons;

    private void OnEnable()
    {
        _slider.value = 1;
        _field.text = ((int)_player.MaxHealth).ToString();

        foreach(var button in _buttons)         
            button.HealthChanged += OnHealthChanged;        
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)       
            button.HealthChanged -= OnHealthChanged;       
    }

    private void OnHealthChanged(float currentHealth)
    {             
        _slider.value = (float)currentHealth / _player.MaxHealth;
        _field.text = ((int)currentHealth).ToString();
    }
}
