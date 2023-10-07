using Sources.Modules.SaveControl.Scripts;
using UnityEngine;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;

namespace Sources.App.Scripts
{
    public class MenuRoot : MonoBehaviour
    {
        [SerializeField] private ScoreWallet _scoreWallet;
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private Saver _Saver;
        [SerializeField] private ShopPanel _shop;
        [SerializeField] private UISwitcher _uiSwitch;

        private void Awake()
        {
            _Saver.Init();
            _tutorial.Init();
            _shop.Init();
            _scoreWallet.Init();
            _uiSwitch.Init();
        }
    }
}
