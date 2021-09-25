using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private int _sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player)) 
        {
            player.Die();
            SceneManager.LoadScene(_sceneBuildIndex);
        }                     
    }
}
