using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class GameManager : MonoBehaviour
    {
        public GameObject EnemyPrefab;

        public Enemy[] Enemies
        {
            get
            {
                return EnemyList.ToArray();
            }
        }
        List<Enemy> EnemyList;
        
        public Waypoint[] Waypoints
        {
            get
            {
                return WaypointArray;
            }
        }
        [SerializeField]
        Waypoint[] WaypointArray;

        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                return _instance;
            }
        }

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            } else {
                _instance = this;
            }
            DontDestroyOnLoad(gameObject);

            EnemyList = new List<Enemy>();
        }

        private void Start()
        {

        }

        public float spawnTimer = 1f;
        private float _lastSpawnTime = 0f;

        private void Update()
        {
            if(Time.time - _lastSpawnTime >= spawnTimer)
            {
                _lastSpawnTime = Time.time;
                Spawn();
            }
        }

        void Spawn()
        {
            GameObject go = Instantiate(EnemyPrefab, Waypoints[0].transform.position, Quaternion.identity);
            EnemyList.Add(go.GetComponent<Enemy>());
        }

        public void RemoveEnemy(Enemy enemy)
        {
            EnemyList.Remove(enemy);
        }
    }
}