using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sources.Modules.SoundControl.Scripts;
using Sources.Modules.UI.Scripts;
using Sources.Modules.Wallet.Scripts;

namespace Sources.Modules.SaveControl.Scripts
{
    public class Saver : MonoBehaviour
    {
        [SerializeField] private ScoreWallet _wallet;
        [SerializeField] private ShopPanel _shop;
        [SerializeField] private SoundVolume _sound;
        [SerializeField] private Tutorial _tutorial;

        public void Init()
        {
            _tutorial.TutorialPassed += SaveTutorialProgress;
            _tutorial.OnEnabled += TrySetTutorialProgress;
            _shop.OnStarted += SetShopParams;
            _shop.SkinChanged += SaveLastEquippedSkin;
            _shop.SkinBought += SaveBoughtSkin;
            _wallet.OnStarted += TrySetBalance;
            _wallet.BalanceChanged += SaveBalance;
            _sound.VolumeChanged += SaveAudioVolume;
            _sound.OnStarted += TrySetAudioVolume;

        }

        private void OnDisable()
        {
            _wallet.BalanceChanged -= SaveBalance;
            _wallet.OnStarted -= TrySetBalance;
            _shop.SkinChanged -= SaveLastEquippedSkin;
            _shop.SkinBought -= SaveBoughtSkin;
            _shop.OnStarted -= SetShopParams;
            _sound.VolumeChanged -= SaveAudioVolume;
            _sound.OnStarted -= TrySetAudioVolume;
            _tutorial.TutorialPassed -= SaveTutorialProgress;
            _tutorial.OnEnabled -= TrySetTutorialProgress;
        }

        private void TrySetTutorialProgress()
        {
            if (PlayerPrefs.HasKey(SaveDataVariables.IsTutorialPassed))
                _tutorial.OnTutorialPassed();
        }
        
        private void SaveTutorialProgress()
        {
            PlayerPrefs.SetInt(SaveDataVariables.IsTutorialPassed, 1);
            PlayerPrefs.Save();
        }
        
        private void TrySetAudioVolume()
        {
            if (PlayerPrefs.HasKey(SaveDataVariables.IsSoundOn))
                _sound.SetVolume(PlayerPrefs.GetInt(SaveDataVariables.IsSoundOn));
        }
        
        private void SaveAudioVolume(bool isSoundOn)
        {
            PlayerPrefs.SetInt(SaveDataVariables.IsSoundOn, isSoundOn ? 1 : 0);
            PlayerPrefs.Save();
        }

        private void TrySetBalance()
        {
            if (PlayerPrefs.HasKey(SaveDataVariables.CurrentBalance))
                _wallet.SetPoints(PlayerPrefs.GetInt(SaveDataVariables.CurrentBalance));
        }

        private void SaveBalance(int balance)
        {
            PlayerPrefs.SetInt(SaveDataVariables.CurrentBalance, balance);
            PlayerPrefs.Save();
        }

        private void SetShopParams()
        {
            TrySetBoughtSkins();
            TrySetSkin();
        }

        private void TrySetSkin()
        {
            if (PlayerPrefs.HasKey(SaveDataVariables.LastEquippedBall))
                _shop.SetLastEquippedSkin(PlayerPrefs.GetInt(SaveDataVariables.LastEquippedBall));
        }
        
        private void SaveLastEquippedSkin(int index)
        {
            PlayerPrefs.SetInt(SaveDataVariables.LastEquippedBall, index);
            PlayerPrefs.Save();
        }

        private void TrySetBoughtSkins()
        {
            if (PlayerPrefs.HasKey(SaveDataVariables.BoughtSkins))
                _shop.SetBoughtSkins(FormListFromStr(PlayerPrefs.GetString(SaveDataVariables.BoughtSkins)));
        }

        private void SaveBoughtSkin(int index)
        {
            PlayerPrefs.SetString(SaveDataVariables.BoughtSkins, AddIndexToStr(index));
            PlayerPrefs.Save();
        }

        private List<int> FormListFromStr(string indexes)
        {
            List<int> boughtIndexes = indexes.Select(indexChar => indexChar - '0').ToList();
            return boughtIndexes;
        }

        private string AddIndexToStr(int index)
        {
            string BoughtSkinsNumbers;

            if (PlayerPrefs.HasKey(SaveDataVariables.BoughtSkins))
            {
                BoughtSkinsNumbers = PlayerPrefs.GetString(SaveDataVariables.BoughtSkins);
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
