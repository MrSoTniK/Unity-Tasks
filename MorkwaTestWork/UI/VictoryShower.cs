using UnityEngine;

public class VictoryShower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Canvas _victoryCanvas;

    private void OnEnable()
    {
        _player.PlayerWon += ShowVictoryCanvas;
    }

    private void OnDisable()
    {
        _player.PlayerWon -= ShowVictoryCanvas;
    }

    private void ShowVictoryCanvas()
    {
        Time.timeScale = 0f;
        _victoryCanvas.gameObject.SetActive(true);
    }
}