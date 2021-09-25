using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private EnemyMovement[] _enemies;
    [SerializeField] private Player _player;
    [SerializeField] private float _minDistanceToPlayer;

    private List<Vector3> _freeTiles = new List<Vector3>();

    public void GenerateEnemies(List<Vector3> freeTiles) 
    {
      //  _enemies = new List<EnemyMovement>();
     //   _enemies = Resources.FindObjectsOfTypeAll<EnemyMovement>().ToList();

        int previousID = freeTiles.Count;
        int startPositionID = 0;
        int currentEnemyNumber = 0;

        while(currentEnemyNumber < _enemies.Length)
        {          
            System.Random rand = new System.Random();
            startPositionID = rand.Next(0, freeTiles.Count);

            Vector3 startPosition = freeTiles[startPositionID];

            if (Vector2.Distance(_player.transform.position, startPosition) >= _minDistanceToPlayer && startPositionID != previousID)
            {
                _enemies[currentEnemyNumber].transform.position = startPosition;
                _enemies[currentEnemyNumber].GetComponent<EnemyMovement>().GenerateWaypoints(freeTiles, startPosition);
                _enemies[currentEnemyNumber].gameObject.SetActive(true);
                previousID = startPositionID;
                currentEnemyNumber++;
            }
        }        
    }
}
