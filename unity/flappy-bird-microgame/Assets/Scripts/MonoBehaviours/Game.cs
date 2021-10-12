using System;
using System.Collections;
using UnityEngine;

namespace MonoBehaviours
{
    public class Game : MonoBehaviour
    {
        public static event Action GameStarted;
        public static event Action<GameObject> PlayerSpawned;
        public static event Action<GameObject> ObstacleSpawned;
        
        public GameObject playerPrefab;
        public GameObject obstacleGroupPrefab;
        public float spawnInterval = 3.0f;
        private GameObject _playerInstance;
        private GameObject _playerSpawn;
        private GameObject _obstacleSpawn;
        private Camera _camera;

        private void OnEnable()
        {
            _playerSpawn = new GameObject {name = "Player Spawner"};
            _obstacleSpawn = new GameObject {name = "Obstacle Spawner"};
            _camera = Camera.main;
            if (_camera)
            {
                _playerSpawn.transform.position = CalculatePlayerSpawn();
                _obstacleSpawn.transform.position = CalculateObstacleSpawn();
            }
            SpawnPlayer();
            StartCoroutine(SpawnInfiniteObstacleGroups());
            Play();
        }

        private IEnumerator SpawnInfiniteObstacleGroups()
        {
            while (true)
            {
                yield return new WaitForSeconds(spawnInterval);
                SpawnObstacleGroup();
            }
        }

        private Vector3 CalculatePlayerSpawn()
        {
            return _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 10));
        }

        private Vector3 CalculateObstacleSpawn()
        {
            return _camera.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 10));
        }

        private void SpawnPlayer()
        {
            if (!playerPrefab) Debug.LogError("No player prefab exists");
            _playerInstance = Instantiate(playerPrefab, _playerSpawn.transform);
            PlayerSpawned?.Invoke(_playerInstance);
        }

        private void SpawnObstacleGroup()
        {
            if (!obstacleGroupPrefab) Debug.LogError("No obstacle group prefab exists");
            var obstacleGroup = Instantiate(obstacleGroupPrefab, _obstacleSpawn.transform);
            ObstacleSpawned?.Invoke(obstacleGroup);
        }

        private void Play()
        {
            GameStarted?.Invoke();
        }
    }
}