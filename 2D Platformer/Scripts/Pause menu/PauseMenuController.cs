using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private KeyCode _menuKey;

    public void OnResumeButtonClick() 
    {
        _menuCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMainMenuButtonClick() 
    {
        MainMenu.Load();
    }

    public void OnExitButtonClick() 
    {
        Application.Quit();
    }

    private void Start()
    {
        _menuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(_menuKey)) 
        {
            Time.timeScale = 0f;
            _menuCanvas.SetActive(true);
        }           
    }
}
