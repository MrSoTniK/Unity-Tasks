using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Renderer))]
public class RoofHide : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private Renderer _roofRenderer;
    private bool _isActive;

    private void Start()
    {
        _isActive = true;
        _roofRenderer = GetComponent<Renderer>();
    }
   
    public void Hide() 
    {
        if (_slider.value == 1.0f && _isActive == true)
        {
            _roofRenderer.enabled = false;
            _isActive = false;
        }

        if (_slider.value == 0.0f && _isActive == false)
        {
            _roofRenderer.enabled = true;
            _isActive = true;
        }
    }
}
