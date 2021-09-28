using IJunior.TypedScenes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameOverMenuController : MonoBehaviour
{
    public void OnTryAgainButtonClick() 
    {
        Game.Load();
    }

    public void OnMainMenuButtonClick()
    {
        MainMenu.Load();
    }

    public void OnExitButtonButtonClick()
    {
        Application.Quit();
    }
}
