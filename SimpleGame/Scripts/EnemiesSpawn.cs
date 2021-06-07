using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy 
{
    public class EnemiesSpawn : MonoBehaviour
    {            
        [SerializeField] private GameObject _template;
        [SerializeField] private Transform[] _spawnZones;
        [SerializeField] private float _maxEnemiesQuantity;
        [SerializeField] private float _pauseBetweenEnemiesSpawn;
        private GameObject _player;
        private bool _isCreatingEnemyCycleWork;
        public static int EnemiesQuantity { get; private set; }

        private GameObject GetPlayer()
        {
            GameObject player = null;
            GameObject[] sceneGameObjects = FindObjectsOfType<GameObject>();

            foreach (var item in sceneGameObjects)
            {
                if (item.TryGetComponent<Player>(out Player playerUnit))
                {
                    player = item;
                    Debug.Log("Success");
                }
            }

            return player;
        }

        private void Start()
        {
            _player = GetPlayer();

            _isCreatingEnemyCycleWork = true;        
            EnemiesQuantity = 0;          
            StartCoroutine(CreateEnemy());
        }    

        private void Update()
        {
            if(!_isCreatingEnemyCycleWork && EnemiesQuantity < _maxEnemiesQuantity && _player != null) 
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
                if (EnemiesQuantity < _maxEnemiesQuantity)
                {
                    int spawnZonesQuantity = _spawnZones.Count();
                    int spawnZoneId = Random.Range(0, spawnZonesQuantity);
                    GameObject newEnemy = Instantiate(_template, _spawnZones[spawnZoneId].position, _template.transform.rotation);
                    EnemiesQuantity++;
                    yield return new WaitForSeconds(_pauseBetweenEnemiesSpawn);
                }
                else 
                {
                    _isCreatingEnemyCycleWork = false;
                }
            }         
        }
    }
}