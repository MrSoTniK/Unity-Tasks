using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoofHide : MonoBehaviour
{
    private Slider _slider;

    void Start()
    {
        _slider = GameObject.FindObjectOfType<Slider>();
    }

    void Update()
    {
        bool isActive = false, isUnactive = false;

        if (_slider.value == 1.0f  && isUnactive == false) 
        {
            gameObject.GetComponent<Renderer>().enabled = false;           
            isUnactive = true;
            isActive = false;
        }

        if(_slider.value == 0.0f  && isActive == false) 
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            isUnactive = false;
            isActive = true;
        }      
    }
}
