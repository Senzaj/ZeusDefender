using Sources.Modules.ObjectFactory.Scripts;
using Sources.Modules.SaveControl.Scripts;
using Sources.Modules.Wallet.Scripts;
using Sources.Modules.UI.Scripts;
using UnityEngine;

namespace Sources.App.Scripts
{
    internal class Root : MonoBehaviour
    {
        [SerializeField] private ObjectsFactory _coinFactory;
        [SerializeField] private ObjectsFactory _bombFactory;
        [SerializeField] private ObjectsFactory _arrowFactory;
        [SerializeField] private ScoreMeter _scoreCounter;
        [SerializeField] private ObjectSpawner _actualSpawner;
        [SerializeField] private ShopPanel _shop;
        [SerializeField] private UISwitcher _uiSwitcher;
        [SerializeField] private ScoreWallet _scoreWallet;
        [SerializeField] private Tutorial _tutor;
        [SerializeField] private Saver _saver;
        
        private void Awake()
        {
            _saver.Init();
            _tutor.Init();
            _shop.Init();
            _scoreWallet.Init();
            _uiSwitcher.Init();
            _coinFactory.Init();
            _bombFactory.Init();
            _arrowFactory.Init();
            _actualSpawner.Init(_coinFactory, _bombFactory, _arrowFactory, _scoreCounter, _tutor);
        }
    }
}
