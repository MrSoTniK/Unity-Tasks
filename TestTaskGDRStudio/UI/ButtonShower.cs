using UnityEngine;
using UnityEngine.UI;

public class ButtonShower : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private Button _restartButton;

    private void OnEnable()
    {
        _counter.AllEnemiesDied += ShowButton;
    }

    private void OnDisable()
    {
        _counter.AllEnemiesDied -= ShowButton;
    }

    private void ShowButton() 
    {
        Time.timeScale = 0f;
        _restartButton.gameObject.SetActive(true);
    }
}