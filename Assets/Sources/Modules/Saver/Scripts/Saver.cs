using System.Collections.Generic;
using System.Linq;
using Sources.Modules.Sounds.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;
using UnityEngine;

namespace Sources.Modules.Saver.Scripts
{
    public class Saver : MonoBehaviour
    {
        [SerializeField] private ScoreWallet _wallet;
        [SerializeField] private ShopPanel _shop;
        [SerializeField] private SoundVolume _sound;
        [SerializeField] private Tutorial _tutorial;

        public void Init()
        {
            _wallet.BalanceChanged += SaveBalance;
            _wallet.OnStarted += TrySetBalance;
            _shop.SkinChanged += SaveLastEquippedBallSkin;
            _shop.SkinBought += SaveBoughtSkin;
            _shop.OnStarted += SetShopParams;
            _sound.VolumeChanged += SaveVolume;
            _sound.OnStarted += TrySetVolume;
            _tutorial.TutorialPassed += SaveTutorialProgress;
            _tutorial.OnEnabled += TrySetTutorialProgress;

        }

        private void OnDisable()
        {
            _wallet.BalanceChanged -= SaveBalance;
            _wallet.OnStarted -= TrySetBalance;
            _shop.SkinChanged -= SaveLastEquippedBallSkin;
            _shop.SkinBought -= SaveBoughtSkin;
            _shop.OnStarted -= SetShopParams;
            _sound.VolumeChanged -= SaveVolume;
            _sound.OnStarted -= TrySetVolume;
            _tutorial.TutorialPassed -= SaveTutorialProgress;
            _tutorial.OnEnabled -= TrySetTutorialProgress;
        }

        private void TrySetTutorialProgress()
        {
            if (PlayerPrefs.HasKey(SaveDataVar.IsTutorialPassed))
                _tutorial.OnTutorialPassed();
        }
        
        private void SaveTutorialProgress()
        {
            PlayerPrefs.SetInt(SaveDataVar.IsTutorialPassed, 1);
            PlayerPrefs.Save();
        }
        
        private void TrySetVolume()
        {
            if (PlayerPrefs.HasKey(SaveDataVar.IsSoundOn))
                _sound.SetVolume(PlayerPrefs.GetInt(SaveDataVar.IsSoundOn));
        }
        
        private void SaveVolume(bool isSoundOn)
        {
            PlayerPrefs.SetInt(SaveDataVar.IsSoundOn, isSoundOn ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void TrySetBalance()
        {
            if (PlayerPrefs.HasKey(SaveDataVar.CurrentBalance))
                _wallet.SetPoints(PlayerPrefs.GetInt(SaveDataVar.CurrentBalance));
        }

        private void SaveBalance(int balance)
        {
            PlayerPrefs.SetInt(SaveDataVar.CurrentBalance, balance);
            PlayerPrefs.Save();
        }

        private void SetShopParams()
        {
            TrySetBoughtSkins();
            TrySetBallSkin();
        }

        private void TrySetBallSkin()
        {
            if (PlayerPrefs.HasKey(SaveDataVar.LastEquippedBall))
                _shop.SetLastEquippedSkin(PlayerPrefs.GetInt(SaveDataVar.LastEquippedBall));
        }
        
        private void SaveLastEquippedBallSkin(int index)
        {
            PlayerPrefs.SetInt(SaveDataVar.LastEquippedBall, index);
            PlayerPrefs.Save();
        }

        private void TrySetBoughtSkins()
        {
            if (PlayerPrefs.HasKey(SaveDataVar.BoughtSkins))
                _shop.SetBoughtSkins(FormListFromString(PlayerPrefs.GetString(SaveDataVar.BoughtSkins)));
        }

        private void SaveBoughtSkin(int index)
        {
            PlayerPrefs.SetString(SaveDataVar.BoughtSkins, AddIndexToString(index));
            PlayerPrefs.Save();
        }

        private List<int> FormListFromString(string indexes)
        {
            List<int> boughtIndexes = indexes.Select(indexChar => indexChar - '0').ToList();
            return boughtIndexes;
        }

        private string AddIndexToString(int index)
        {
            string BoughtSkinsNumbers;

            if (PlayerPrefs.HasKey(SaveDataVar.BoughtSkins))
            {
                BoughtSkinsNumbers = PlayerPrefs.GetString(SaveDataVar.BoughtSkins);
                BoughtSkinsNumbers += index.ToString();
            }
            else
            {
                BoughtSkinsNumbers = index.ToString();
            }

            return BoughtSkinsNumbers;
        }
    }
}
