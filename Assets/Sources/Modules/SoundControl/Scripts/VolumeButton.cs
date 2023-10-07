using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.SoundControl.Scripts
{
    public class VolumeButton : MonoBehaviour
    {
        [SerializeField] private Sprite _enabledImage;
        [SerializeField] private Sprite _disabledImage;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        public event Action Pressed;

        private void OnEnable()
        {
            _button.onClick.AddListener((() => Pressed?.Invoke()));
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener((() => Pressed?.Invoke()));
        }

        public void OnSoundEnabled() => _image.sprite = _enabledImage;

        public void OnSoundDisabled() => _image.sprite = _disabledImage;
    }
}
