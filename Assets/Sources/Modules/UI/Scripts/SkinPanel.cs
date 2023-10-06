using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class SkinPanel : MonoBehaviour
    {
        [SerializeField] private int _index;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private int _price;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private CanvasGroup _buyButtonCanvas;
        [SerializeField] private Button _equipButton;
        [SerializeField] private CanvasGroup _equipButtonCanvas;
        [SerializeField] private TMP_Text _equipButtonText;
        [SerializeField] private bool _isBought;
        [SerializeField] private bool _isEquipped;

        public event Action<SkinPanel, Sprite> SpriteChangingRequest;
        public event Action<SkinPanel, int> BuyRequest;

        private const string EquippedText = "Equipped";
        private const string UnequippedText = "Unquipped";

        public int Index => _index;
        public Sprite ChangingSprite => _sprite;

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
                    Unequip();
            }
            else
            {
                ShowCanvas(_buyButtonCanvas);
                HideCanvas(_equipButtonCanvas);
            }
            
            _buyButton.onClick.AddListener(TryBuy);
            _equipButton.onClick.AddListener(Equip);
        }

        private void OnDisable()
        {
            _buyButton.onClick.RemoveListener(TryBuy);
            _equipButton.onClick.RemoveListener(Equip);
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
            _equipButtonText.text = EquippedText;
        }
        
        public void Unequip()
        {
            _isEquipped = false;
            _equipButtonText.text = UnequippedText;
        }

        private void TryBuy()
        {
            BuyRequest?.Invoke(this, _price);
        }
        
        private void Equip()
        {
            SpriteChangingRequest?.Invoke(this, _sprite);
        }

        private void SetPrice()
        {
            _priceText.text = _price.ToString();
        }
        
        private void ShowCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        
        private void HideCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
