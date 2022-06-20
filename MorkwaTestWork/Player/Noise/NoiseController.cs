using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class NoiseController : MonoBehaviour
{
    [SerializeField] private NoiseChanger _noiseIncreaser;
    [SerializeField] private NoiseChanger _noiseReducer;
    [SerializeField] private float _maxNoiseValue;

    private Rigidbody _rigidBody;  
    public float ReceivedNoiseValue { get; private set; }
    public float MaxNoiseValue => _maxNoiseValue;

    public UnityAction MaxNoiseWasReached;
    public UnityAction<float> NoiseValueWasChanged;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();      
    }

    private void OnEnable()
    {
        _noiseIncreaser.NoiseValueChanged += ReceiveNoiseValue;
        _noiseReducer.NoiseValueChanged += ReceiveNoiseValue;
        ReceivedNoiseValue = 0;
    }

    private void OnDisable()
    {
        _noiseIncreaser.NoiseValueChanged -= ReceiveNoiseValue;
        _noiseReducer.NoiseValueChanged -= ReceiveNoiseValue;
    }

    private void Update()
    {
        if (_rigidBody.velocity != Vector3.zero)
            _noiseIncreaser.enabled = true;
        else
            _noiseIncreaser.enabled = false;
       
        if (ReceivedNoiseValue > 0 && !_noiseIncreaser.enabled)
            _noiseReducer.enabled = true;
        else 
            _noiseReducer.enabled = false;             
    }

    private void ReceiveNoiseValue(float noiseValue) 
    {
        ReceivedNoiseValue = Mathf.Clamp(noiseValue, 0, _maxNoiseValue);
        NoiseValueWasChanged.Invoke(ReceivedNoiseValue);
        if (ReceivedNoiseValue >= _maxNoiseValue) 
        {
            MaxNoiseWasReached?.Invoke();
            _noiseIncreaser.enabled = false;
            this.enabled = false;
        }          
    }
}