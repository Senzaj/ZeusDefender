using UnityEngine;
using System.Collections.Generic;
using Sources.Modules.Player.Scripts;
using Sources.Modules.Wallet.Scripts;
using System;
using Sources.Modules.Player.Scripts.Weapon;

namespace Sources.Modules.UI.Scripts
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private SkinPanel[] _panels;
        [SerializeField] private PlayerSkin _player;
        [SerializeField] private ScoreWallet _wallet;

        public event Action OnStarted;
        public event Action<int> SkinBought; 
        public event Action<int> SkinChanged;

        public void Init()
        {
            foreach (SkinPanel panel in _panels)
            {
                panel.PurchaseRequest += TryPurchase;
                panel.SpriteChangingRequest += TryEquipSkin;
            }
            
            OnStarted?.Invoke();
        }

        private void OnDisable()
        {
            foreach (SkinPanel panel in _panels)
            {
                panel.PurchaseRequest -= TryPurchase;
                panel.SpriteChangingRequest -= TryEquipSkin;
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
        
        public void SetLastEquippedSkin(int skinIndex)
        {
            foreach (SkinPanel panel in _panels)
            {
                if (panel.Index == skinIndex)
                {
                    TryEquipSkin(panel, panel.ChangingWeapon);
                    break;
                }
            }
        }
        
        private void TryPurchase(SkinPanel panel, int price)
        {
            if (_wallet.IsPointsEnough(price))
            {
                _wallet.RemovePoints(price);
                panel.OnBought();
                SkinBought?.Invoke(panel.Index);
            }
        }

        private void TryEquipSkin(SkinPanel panel, PlayerWeapon weaponPrefab)
        {
            foreach (SkinPanel ballPanel in _panels)
                ballPanel.Unequipped();
            
            if (_player != null)
                _player.ChangeWeapon(weaponPrefab);
            
            panel.OnEquipped();
            
            SkinChanged?.Invoke(panel.Index);
        }
    }
}
