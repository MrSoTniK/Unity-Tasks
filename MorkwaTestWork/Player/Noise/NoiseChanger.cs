using UnityEngine;
using UnityEngine.Events;

public class NoiseChanger : MonoBehaviour
{
    [SerializeField] private float _maxNoisePerFrame;
    [SerializeField] private NoiseController _controller;
   
    private float _noiseValue;

    public UnityAction<float> NoiseValueChanged;

    private void OnEnable()
    {
        _noiseValue = _controller.ReceivedNoiseValue;
    }

    private void FixedUpdate()
    {     
        _noiseValue += _maxNoisePerFrame * Time.fixedDeltaTime; ;
        NoiseValueChanged?.Invoke(_noiseValue);
    }
}
