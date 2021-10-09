using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using IJunior.TypedScenes;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _aboutButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private TMP_Text _textAboutAuthors;
    [SerializeField] private Button _backButton;

    private bool _wasAboutButtonPressed;

    private void Start()
    {
        _textAboutAuthors.gameObject.SetActive(false);
        _backButton.gameObject.SetActive(false);
        _wasAboutButtonPressed = false;
    } 

    public void OnPlayButtonClick() 
    {
        Game.Load();
    }

    public void OnAboutOrBackButtonClick()
    {
        _playButton.gameObject.SetActive(_wasAboutButtonPressed);
        _aboutButton.gameObject.SetActive(_wasAboutButtonPressed);
        _exitButton.gameObject.SetActive(_wasAboutButtonPressed);
        _textAboutAuthors.gameObject.SetActive(!_wasAboutButtonPressed);
        _backButton.gameObject.SetActive(!_wasAboutButtonPressed);
        _wasAboutButtonPressed = !_wasAboutButtonPressed;
    }

    public void OnExitButtonClick()
    {
        Debug.Log("Quit");
        Application.Quit();      
    }
}
