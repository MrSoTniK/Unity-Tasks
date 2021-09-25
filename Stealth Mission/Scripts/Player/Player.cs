using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private int _sceneBuildIndex;

    public void Die()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(_sceneBuildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            Die();
    }
}