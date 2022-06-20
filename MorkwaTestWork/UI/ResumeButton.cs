using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ResumeButton : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Resume);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Resume);
    }

    private void Resume() 
    {
        Time.timeScale = 1f;
        _canvas.gameObject.SetActive(false);
    }
}