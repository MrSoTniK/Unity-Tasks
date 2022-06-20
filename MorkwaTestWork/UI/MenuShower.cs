using UnityEngine;

public class MenuShower : MonoBehaviour
{
    [SerializeField] private Canvas _menuCanvas;

    private InputControl _inputControl;

    private void Awake()
    {
        _inputControl = new InputControl();
    }

    private void OnEnable()
    {
        _inputControl.Enable();
        _inputControl.UI.GoToMenu.performed += ctx => ShowMenu();
    }

    private void OnDisable()
    {
        _inputControl.UI.GoToMenu.performed -= ctx => ShowMenu();
        _inputControl.Disable();
    }

    private void ShowMenu() 
    {
        Time.timeScale = 0f;
        _menuCanvas.gameObject.SetActive(true);
    }
}