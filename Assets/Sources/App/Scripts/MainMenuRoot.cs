using Sources.Modules.Saver.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;
using UnityEngine;

namespace Sources.App.Scripts
{
    public class MainMenuRoot : MonoBehaviour
    {
        [SerializeField] private UISwitcher _uiSwitcher;
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private ScoreWallet _scoreWallet;
        [SerializeField] private ShopPanel _shop;
        [SerializeField] private Saver _Saver;

        private void Awake()
        {
            _Saver.Init();
            _tutorial.Init();
            _shop.Init();
            _scoreWallet.Init();
            _uiSwitcher.Init();
        }
    }
}
