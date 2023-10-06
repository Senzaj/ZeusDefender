using System.Collections;
using Sources.Modules.Player.Scripts;
using Sources.Modules.SpawnedObject.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.ObjectFactory.Scripts
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private Vector2[] _spawnPoints;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private PlayerSkin _playerSkin;
        [SerializeField] private LoseZone[] _zones;
        [SerializeField] private Panel _inGamePanel;

        private const float MinSpawnCooldown = 3;
        private const float MaxSpawnCooldown = 6;
        private const float WaitBeforeLose = 0.7f;
        
        private Tutorial _tutorial;
        private ObjectsFactory _coinFactory;
        private ObjectsFactory _obstacleFactory;
        private ObjectsFactory[] _factories;
        private ScoreCounter _scoreCounter;
        
        private Coroutine _spawnWork;
        private bool _canSpawn;
        private bool _playerAlreadyLose;

        public void Init(ObjectsFactory coinFactory,ObjectsFactory obstacleFactory , ScoreCounter scoreCounter, Tutorial tutorial)
        {
            _coinFactory = coinFactory;
            _obstacleFactory = obstacleFactory;
            _scoreCounter = scoreCounter;
            _tutorial = tutorial;

            _factories = new[] {_coinFactory, _obstacleFactory };
            
            _tutorial.TutorialPassed += StartSpawning;

            foreach (var zone in _zones)
                zone.OnLoseZoneEntered += OnLose;

            _playerAlreadyLose = false;

            StartSpawning();
        }

        private void OnDisable()
        {
            _tutorial.TutorialPassed -= StartSpawning;
            
            foreach (var zone in _zones)
                zone.OnLoseZoneEntered -= OnLose;
            
            StopSpawning();
        }

        private void OnLose()
        {
            if (_playerAlreadyLose == false)
            {
                _inGamePanel.TurnOff();
                _playerAlreadyLose = true;
                _playerSkin.OnDeath();
                StartCoroutine(OnPlayerDefeated());
            }
        }
        
        private void OnLose(Obstacle obstacle)
        {
            if (_playerAlreadyLose == false)
            {
                _inGamePanel.TurnOff();
                RemoveObs(obstacle);
                _playerAlreadyLose = true;
                _playerSkin.OnDeath();
                StartCoroutine(OnPlayerDefeated());
            }
        }
        
        private IEnumerator OnPlayerDefeated()
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(WaitBeforeLose);
            _losePanel.SetScore();
            yield return waitForSeconds;
            _losePanel.GetComponent<Panel>().TurnOn();
        }
        
        private void RemoveObs(Obstacle obstacle)
        {
            obstacle.PlayerDefeated -= OnLose;
            obstacle.Skipped -= RemoveObs;
        }
        
        private void StartSpawning()
        {
            if (_tutorial.IsTutorialPassed)
            {
                _canSpawn = true;
                _spawnWork = StartCoroutine(Spawning());
            }
            else
            {
                _tutorial.Begin();
            }
        }

        private void StopSpawning()
        {
            if (_spawnWork != null)
                StopCoroutine(_spawnWork);

            _canSpawn = false;
        }
    
        private IEnumerator Spawning()
        {
            while (_canSpawn)
            {
                WaitForSeconds waitForSeconds = new WaitForSeconds(Random.Range(MinSpawnCooldown, MaxSpawnCooldown));

                int index = Random.Range(0, _factories.Length);

                CommonObject newObject = _factories[index].GetObject();

                if (newObject.TryGetComponent(out Coin newCoin))
                    _scoreCounter.AddCoin(newCoin);

                if (newObject.TryGetComponent(out Obstacle newObstacle))
                {
                    newObstacle.PlayerDefeated += OnLose;
                    newObstacle.Skipped += RemoveObs;
                }

                SpawnAt(newObject, _spawnPoints[Random.Range(0, _spawnPoints.Length)]);

                yield return waitForSeconds;
            }
        }
        
        private void SpawnAt(CommonObject spawnedObj ,Vector2 spawnPos)
        {
            spawnedObj.gameObject.SetActive(true);
            spawnedObj.transform.position = spawnPos;
        }
    }
}
