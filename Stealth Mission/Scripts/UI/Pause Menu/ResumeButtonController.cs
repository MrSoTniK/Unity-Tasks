using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButtonController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    public void OnClick() 
    {
        _pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
