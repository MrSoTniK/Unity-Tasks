using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class NoiseSlider : MonoBehaviour
{
    [SerializeField] private NoiseController _controller;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _controller.NoiseValueWasChanged += SetSliderValue;
        _slider.value = 0;
        _slider.maxValue = _controller.MaxNoiseValue;
    }

    private void OnDisable()
    {
        _controller.NoiseValueWasChanged -= SetSliderValue;
    }

    private void SetSliderValue(float noiseValue) 
    {
        _slider.value = noiseValue;
    }
}