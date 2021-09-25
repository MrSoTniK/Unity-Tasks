using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    public void Start()
    {
        _pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) 
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }          
    }
}
