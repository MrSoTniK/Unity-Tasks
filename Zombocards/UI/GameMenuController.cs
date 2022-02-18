using UnityEngine;
using IJunior.TypedScenes;

public class GameMenuController : MonoBehaviour
{
    [SerializeField] private Canvas _menuCanvas;

    public void OnResumeButtonClick() 
    {
        Time.timeScale = 1f;
        _menuCanvas.gameObject.SetActive(false);
    }

    public void OnExitButtonClick() 
    {
        Application.Quit();
    }

    public void OnMenuButtonClick() 
    {
        MainMenu.Load();
    }
}