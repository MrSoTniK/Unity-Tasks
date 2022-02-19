using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public event UnityAction EnemyDied;

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.TryGetComponent<Enemy>(out Enemy enemy)) 
        {
            Destroy(enemy.gameObject);
            EnemyDied?.Invoke();
        }
    }
}
