using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class NoiseSensor : MonoBehaviour
{
    [SerializeField] private float _maxNoiseValue;
    [SerializeField] private float _stopTimeForNoiseDecrease;
    [SerializeField] private Slider _noiseSlider;
    [SerializeField] private TMP_Text _noiseField;
    [SerializeField] private float _noiseIncreasePerFrame;
    [SerializeField] private float _stopTimeIncreasePerFrame;
    [SerializeField] private float _noiseDecreasePerFrame;

    public UnityEvent OnMaximumNoise;
    public float NoiseValue { get; private set; }

    private float _stopTime = 0;
    private bool _isMaxNoiseReached;

    public void GetNoiseValue(float noiseValue) 
    {      
        if(noiseValue != 0) 
        {
            NoiseValue += Time.deltaTime * _noiseIncreasePerFrame;
            _stopTime = 0;
        }
        else       
            _stopTime += Time.deltaTime * _stopTimeIncreasePerFrame;
        
        if(NoiseValue >= _maxNoiseValue) 
        {
            NoiseValue = _maxNoiseValue;
            _isMaxNoiseReached = true;
            OnMaximumNoise.Invoke();
        }

        if(_stopTime >= _stopTimeForNoiseDecrease && NoiseValue > 0) 
        {
            NoiseValue -= Time.deltaTime * _noiseDecreasePerFrame;
            if (NoiseValue < 0)
                NoiseValue = 0;
        }           
    }

    private void Start()
    {
        NoiseValue = 0;
        _isMaxNoiseReached = false;
    }

    private void Update()
    {
        if (!_isMaxNoiseReached) 
        {
            _noiseField.text = System.Math.Round(NoiseValue, 1).ToString();
            _noiseSlider.value = (float)System.Math.Round(NoiseValue, 1);
        }
    }
}
