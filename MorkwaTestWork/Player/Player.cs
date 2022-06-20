using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityAction PlayerDied;
    public UnityAction PlayerWon;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            Die();

        if (collision.TryGetComponent<Exit>(out Exit exit))
            Win();
    }

    private void Die()
    {
        PlayerDied?.Invoke();
        gameObject.SetActive(false);
    }

    private void Win()
    {
        PlayerWon?.Invoke();
        gameObject.SetActive(false);
    }
}