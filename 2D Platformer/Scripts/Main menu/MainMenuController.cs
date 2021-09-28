using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _firstMenuLayer;
    [SerializeField] private GameObject _secondMenuLayer;

    public void OnPlayButtonClick() 
    {
        _firstMenuLayer.SetActive(false);
        _secondMenuLayer.SetActive(true);
    }

    public void OnBackButtonClick() 
    {
        _secondMenuLayer.SetActive(false);
        _firstMenuLayer.SetActive(true);     
    }

    public void OnExitButtonClick() 
    {
        Application.Quit();
    }

    public void OnLevel1ButtonClick() 
    {
        Game.Load();
    }

    private void Start()
    {
        _secondMenuLayer.SetActive(false);
    }
}
