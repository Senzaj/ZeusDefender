using System;
using Sources.Modules.Player.Scripts.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class SkinPanel : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private PlayerWeapon _weapon;
        [SerializeField] private int _price;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private CanvasGroup _buyButtonCanvas;
        [SerializeField] private Button _equipButton;
        [SerializeField] private CanvasGroup _equipButtonCanvas;
        [SerializeField] private TMP_Text _equipButtonText;
        [SerializeField] private bool _isBought;
        [SerializeField] private bool _isEquipped;

        public event Action<SkinPanel, PlayerWeapon> SpriteChangingRequest;
        public event Action<SkinPanel, int> PurchaseRequest;

        private const string EquippedTxt = "Equipped";
        private const string UnequippedTxt = "Unquipped";

        public int Index => _index;
        public PlayerWeapon ChangingWeapon => _weapon;

        private void OnEnable()
        {
            SetPrice();

            if (_isBought)
            {
                ShowCanvas(_equipButtonCanvas);
                HideCanvas(_buyButtonCanvas);

                if (_isEquipped)
                    OnEquipped();
                else
                    Unequipped();
            }
            else
            {
                ShowCanvas(_buyButtonCanvas);
                HideCanvas(_equipButtonCanvas);
            }

            _equipButton.onClick.AddListener(Equip);
            _buyButton.onClick.AddListener(TryBuy);
        }

        private void OnDisable()
        {
            _equipButton.onClick.RemoveListener(Equip);
            _buyButton.onClick.RemoveListener(TryBuy);
        }

        public void OnBought()
        {
            _isBought = true;
            HideCanvas(_buyButtonCanvas);
            ShowCanvas(_equipButtonCanvas);
            Equip();
        }

        public void OnBoughtWithoutEquip()
        {
            _isBought = true;
            HideCanvas(_buyButtonCanvas);
            ShowCanvas(_equipButtonCanvas);
        }

        public void OnEquipped()
        {
            _isEquipped = true;
            _equipButtonText.text = EquippedTxt;
        }

        public void Unequipped()
        {
            _isEquipped = false;
            _equipButtonText.text = UnequippedTxt;
        }

        private void SetPrice() => _priceText.text = _price.ToString();
        
        private void TryBuy() => PurchaseRequest?.Invoke(this, _price);

        private void Equip() => SpriteChangingRequest?.Invoke(this, _weapon);

        private void ShowCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        private void HideCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}
