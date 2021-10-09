using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextAnimator : MonoBehaviour
{
    [SerializeField] private Color _firstColor;
    [SerializeField] private Color _secondColor;
    [SerializeField] private float _speed;

    private TMP_Text _textField;
    private Color _currentColor;
    private Color _targetColor;
    private Vector3 _currentVelocity;
    private float _elapsedTime;

    private void Start()
    {
        _textField = GetComponent<TMP_Text>();
        _currentColor = _firstColor;
        _targetColor = _secondColor;
        _elapsedTime = 0;
    }

    private void FixedUpdate()
    {      
        _textField.color = Color.Lerp(_currentColor, _targetColor, _speed * _elapsedTime);
        _elapsedTime += Time.deltaTime;

        if (_textField.color == _targetColor) 
        {
            Color colorForChange = _currentColor;
            _currentColor = _targetColor;
            _targetColor = colorForChange;
            _elapsedTime = 0;
        }
    }
}
