using UnityEngine;

public class GameOverShower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Canvas _gameOverCanvas;

    private void OnEnable()
    {
        _player.PlayerDied += ShowGameOverCanvas;
    }

    private void OnDisable()
    {
        _player.PlayerDied -= ShowGameOverCanvas;
    }

    private void ShowGameOverCanvas() 
    {
        Time.timeScale = 0f;
        _gameOverCanvas.gameObject.SetActive(true);
    }
}