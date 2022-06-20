using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class NoiseText : MonoBehaviour
{
    [SerializeField] private NoiseController _controller;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        _controller.NoiseValueWasChanged += ChangeText;
        _text.text = "0";
    }

    private void OnDisable()
    {
        _controller.NoiseValueWasChanged -= ChangeText;
    }

    private void ChangeText(float noiseValue) 
    {
        _text.text = ((int)noiseValue).ToString();
    }
}