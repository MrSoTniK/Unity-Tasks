using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private Button _restartButton;

    private void OnEnable()
    {
        _restartButton = GetComponent<Button>();
        _restartButton.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}