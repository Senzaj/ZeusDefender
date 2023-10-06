using System;
using System.Collections.Generic;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Wallet.Scripts;
using UnityEngine;

namespace Sources.Modules.UI.Scripts
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private SkinPanel[] _panels;
        [SerializeField] private PlayerSkin _player;
        [SerializeField] private ScoreWallet _wallet;

        public event Action<int> SkinChanged;
        public event Action OnStarted;
        public event Action<int> SkinBought; 

        public void Init()
        {
            foreach (SkinPanel panel in _panels)
            {
                panel.BuyRequest += TryBuy;
                panel.SpriteChangingRequest += EquipSkin;
            }
            
            OnStarted?.Invoke();
        }

        private void OnDisable()
        {
            foreach (SkinPanel panel in _panels)
            {
                panel.BuyRequest -= TryBuy;
                panel.SpriteChangingRequest -= EquipSkin;
            }
        }

        public void SetBoughtSkins(List<int> indexes)
        {
            foreach (SkinPanel panel in _panels)
            {
                foreach (int index in indexes)
                {
                    if (panel.Index == index)
                        panel.OnBoughtWithoutEquip();
                }
            }
        }
        
        public void SetLastEquippedSkin(int index)
        {
            foreach (SkinPanel panel in _panels)
            {
                if (panel.Index == index)
                {
                    EquipSkin(panel, panel.ChangingSprite);
                    break;
                }
            }
        }
        
        private void TryBuy(SkinPanel panel, int price)
        {
            if (_wallet.IsPointsEnough(price))
            {
                _wallet.RemovePoints(price);
                panel.OnBought();
                SkinBought?.Invoke(panel.Index);
            }
        }

        private void EquipSkin(SkinPanel panel, Sprite newColor)
        {
            foreach (SkinPanel ballPanel in _panels)
                ballPanel.Unequip();
            
            if (_player != null)
                _player.ChangeSkin(newColor);
            
            panel.OnEquipped();
            SkinChanged?.Invoke(panel.Index);
        }
    }
}
