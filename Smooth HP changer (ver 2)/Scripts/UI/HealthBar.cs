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

    private void OnEnable()
    {
        _slider.value = 1;
        _field.text = ((int)_player.MaxHealth).ToString();
    }

    public void OnHealthChanged(float currentHealth)
    {             
        _slider.value = (float)currentHealth / _player.MaxHealth;
        _field.text = ((int)currentHealth).ToString();
    }
}