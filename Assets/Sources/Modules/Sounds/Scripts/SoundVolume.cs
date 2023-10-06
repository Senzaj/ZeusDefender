using System;
using UnityEngine;

namespace Sources.Modules.Sounds.Scripts
{
    public class SoundVolume : MonoBehaviour
    {
        [SerializeField] private VolumeButton[] _volumeButtons;

        public event Action<bool> VolumeChanged;
        public event Action OnStarted; 

        private const int MinValue = 0;
        private const int MaxValue = 1;

        private void OnEnable()
        {
            foreach (VolumeButton volumeButton in _volumeButtons)
                volumeButton.Clicked += SwitchVolume;
            
            OnStarted?.Invoke();
        }

        private void Start()
        {
            if (AudioListener.volume == MaxValue)
                EnableButtons();
            else
                DisableButtons();
        }

        private void OnDisable()
        {
            foreach (VolumeButton volumeButton in _volumeButtons)
                volumeButton.Clicked -= SwitchVolume;
        }

        public void SetVolume(int isSoundOn)
        {
            if (isSoundOn == 1)
            {
                AudioListener.volume = MaxValue;
                EnableButtons();  
            }
            else
            {
                AudioListener.volume = MinValue;
                DisableButtons();
            }
        }

        private void SwitchVolume()
        {
            if (AudioListener.volume == MaxValue)
                TurnOffVolume();
            else
                TurnOnVolume();
        }

        private void TurnOffVolume()
        {
            AudioListener.volume = MinValue;
            DisableButtons();
            VolumeChanged?.Invoke(false);
        }

        private void TurnOnVolume()
        {
            AudioListener.volume = MaxValue;
            EnableButtons();
            VolumeChanged?.Invoke(true);
        }
        
        private void DisableButtons()
        {
            foreach (VolumeButton button in _volumeButtons)
                button.OnSoundDisabled();
        }
        
        private void EnableButtons()
        {
            foreach (VolumeButton button in _volumeButtons)
                button.OnSoundEnabled();
        }
    }
}
