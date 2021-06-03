using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy 
{
    public class EnemiesSpawn : MonoBehaviour
    {            
        [SerializeField] private GameObject _template;
        private GameObject[] _spawnZones;
        private GameObject _player;
        private bool _isCreatingEnemyCycleWork;
        public static int EnemiesQuantity { get; private set; }

        private void Start()
        {
            _player = GameObject.Find("Player");
            _isCreatingEnemyCycleWork = true;
            _spawnZones = GameObject.FindGameObjectsWithTag("SpawnZone");
            EnemiesQuantity = 0;          
            StartCoroutine(CreateEnemy());
        }    

        private void Update()
        {
            if(!_isCreatingEnemyCycleWork && EnemiesQuantity < 11 && _player != null) 
            {
                _isCreatingEnemyCycleWork = true;
                StartCoroutine(CreateEnemy());
            }
        }

        static public void Decrease() 
        {
            EnemiesQuantity--;
        }

        IEnumerator CreateEnemy()
        {
            while (_isCreatingEnemyCycleWork && _player != null) 
            {
                if (EnemiesQuantity <= 11)
                {
                    int spawnZoneId = Random.Range(0, 3);
                    GameObject newEnemy = Instantiate(_template, _spawnZones[spawnZoneId].transform.position, _template.transform.rotation);
                    EnemiesQuantity++;
                    yield return new WaitForSeconds(2);
                }
                else 
                {
                    _isCreatingEnemyCycleWork = false;
                }
            }         
        }
    }
}

