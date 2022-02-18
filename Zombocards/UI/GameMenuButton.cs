using UnityEngine;
using UnityEngine.UI;

public class GameMenuButton : MonoBehaviour
{
    [SerializeField] private Canvas _menuCanvas;
    [SerializeField] private Button _button;

    public void OnClick() 
    {
        Time.timeScale = 0;
        _menuCanvas.gameObject.SetActive(true);
    }

    public void SetInactive() 
    {
        _button.interactable = false;
    }
}