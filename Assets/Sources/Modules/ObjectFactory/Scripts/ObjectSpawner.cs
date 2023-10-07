using System.Collections;
using Sources.Modules.CommonMovingObject.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources.Modules.ObjectFactory.Scripts
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] private Vector2[] _spawnPoints;
        [SerializeField] private Vector2[] _leftSpawnPoints;
        [SerializeField] private Vector2[] _rightSpawnPoints;
        [SerializeField] private LosePanel _losePanel;
        [SerializeField] private Panel _inGamePanel;

        private const float MinSpawnCooldown = 1f;
        private const float MaxSpawnCooldown = 1.8f;
        private const float MinHorizontalSpawnCd = 0.7f;
        private const float MaxHorizontalSpawnCd = 2;
        private const float WaitBeforeLose = 0.7f;
        
        private Tutorial _tutorial;
        private ObjectsFactory _coinFactory;
        private ObjectsFactory _obstacleFactory;
        private ObjectsFactory _arrowFactory;
        private ObjectsFactory[] _horizontalFactories;
        private ScoreMeter _scoreCounter;
        
        private Coroutine _spawnWork;
        private Coroutine _horizontalSpawning;
        private bool _canSpawn;
        private bool _canHorizontalSpawn;
        private bool _playerAlreadyLose;

        public void Init(ObjectsFactory coinFactory, ObjectsFactory obstacleFactory, ObjectsFactory arrowFactory,
            ScoreMeter scoreMeter, Tutorial tutorial)
        {
            _coinFactory = coinFactory;
            _obstacleFactory = obstacleFactory;
            _arrowFactory = arrowFactory;
            _scoreCounter = scoreMeter;
            _tutorial = tutorial;

            _horizontalFactories = new[] { _coinFactory, _obstacleFactory };

            _tutorial.TutorialPassed += StartSpawning;

            _playerAlreadyLose = false;

            StartSpawning();
        }

        private void OnDisable()
        {
            _tutorial.TutorialPassed -= StartSpawning;
            StopSpawning();
        }

        private void OnLose()
        {
            if (_playerAlreadyLose == false)
            {
                _inGamePanel.TurnOff();
                _playerAlreadyLose = true;
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
                StartCoroutine(OnPlayerDefeated());
            }
        }
        
        private void OnLose(Arrow arrow)
        {
            if (_playerAlreadyLose == false)
            {
                _inGamePanel.TurnOff();
                RemoveArrow(arrow);
                _playerAlreadyLose = true;
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

        private void RemoveArrow(Arrow arrow)
        {
            arrow.PlayerDefeated -= OnLose;
            arrow.Exploded -= RemoveArrow;
        }
        
        private void StartSpawning()
        {
            if (_tutorial.IsTutorialPassed)
            {
                _canSpawn = true;
                _spawnWork = StartCoroutine(Spawning());
                _canHorizontalSpawn = true;
                _horizontalSpawning = StartCoroutine(HorizontalSpawning());
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
            
            if (_horizontalSpawning != null)
                StopCoroutine(_horizontalSpawning);

            _canHorizontalSpawn = false;
        }

        private IEnumerator HorizontalSpawning()
        {
            while (_canHorizontalSpawn)
            {
                WaitForSeconds waitForSeconds = new WaitForSeconds(Random.Range(MinHorizontalSpawnCd, MaxHorizontalSpawnCd));

                int index = Random.Range(0, _horizontalFactories.Length);

                CommonObject newObject = _horizontalFactories[index].GetObject();

                if (newObject.TryGetComponent(out Coin newCoin))
                    _scoreCounter.AddPoint(newCoin);

                if (newObject.TryGetComponent(out Obstacle newObstacle))
                {
                    newObstacle.PlayerDefeated += OnLose;
                    newObstacle.Skipped += RemoveObs;
                }
                
                int rightLeft = Random.Range(0, 2);

                if (rightLeft == 0)
                {
                    SpawnAt(newObject, _rightSpawnPoints[Random.Range(0, _rightSpawnPoints.Length)]);
                    newObject.StartMovement(Vector2.left);
                }
                else
                {
                    SpawnAt(newObject, _leftSpawnPoints[Random.Range(0, _leftSpawnPoints.Length)]);
                    newObject.StartMovement(Vector2.right);
                }
                
                yield return waitForSeconds;
            }
        }
        
        private IEnumerator Spawning()
        {
            while (_canSpawn)
            {
                WaitForSeconds waitForSeconds = new WaitForSeconds(Random.Range(MinSpawnCooldown, MaxSpawnCooldown));

                int index = Random.Range(0, _horizontalFactories.Length);

                CommonObject newObject = _arrowFactory.GetObject();
                Arrow arrow = newObject.GetComponent<Arrow>();
                
                arrow.PlayerDefeated += OnLose;
                arrow.Exploded += RemoveArrow;
                
                SpawnAt(newObject, _spawnPoints[Random.Range(0, _spawnPoints.Length)]);
                newObject.StartMovement(Vector2.down);

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
