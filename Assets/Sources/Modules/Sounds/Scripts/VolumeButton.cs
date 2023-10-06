using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.Sounds.Scripts
{
    public class VolumeButton : MonoBehaviour
    {
        [SerializeField] private Sprite _enabledImage;
        [SerializeField] private Sprite _disabledImage;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        public event Action Clicked; 

        private void OnEnable()
        {
            _button.onClick.AddListener((() => Clicked?.Invoke()));
        }
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener((() => Clicked?.Invoke()));
        }

        public void OnSoundEnabled()
        {
            _image.sprite = _enabledImage;
        }

        public void OnSoundDisabled()
        {
            _image.sprite = _disabledImage;
        }
    }
}
