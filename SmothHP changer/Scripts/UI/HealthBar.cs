using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _field;
    [SerializeField] private Player _player;   

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _slider.value = 1;
        _field.text = ((int)_player.MaxHealth).ToString();
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    public void OnHealthChanged(float value, float maxValue)
    {
        _slider.value = (float)value / maxValue;
        _field.text = ((int)value).ToString();
    }
}
