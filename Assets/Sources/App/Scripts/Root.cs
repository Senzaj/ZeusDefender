using Sources.Modules.ObjectFactory.Scripts;
using Sources.Modules.Saver.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;
using UnityEngine;

namespace Sources.App.Scripts
{
    internal class Root : MonoBehaviour
    {
        [SerializeField] private ObjectsFactory _coinFactory;
        [SerializeField] private ObjectsFactory _obstacleFactory; 
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private ObjectSpawner _spawner;
        [SerializeField] private UISwitcher _uiSwitcher;
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private ShopPanel _shop;
        [SerializeField] private ScoreWallet _scoreWallet;
        [SerializeField] private Saver _saver;
        
        private void Awake()
        {
            _saver.Init();
            _tutorial.Init();
            _shop.Init();
            _scoreWallet.Init();
            _uiSwitcher.Init();
            _coinFactory.Init();
            _obstacleFactory.Init();
            _spawner.Init(_coinFactory, _obstacleFactory, _scoreCounter, _tutorial);
        }
    }
}
