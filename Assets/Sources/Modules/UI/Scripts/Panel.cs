using System;
using Sources.Modules.Player.Scripts.Meteor;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private bool _isInGamePanel;
        [SerializeField] private bool _isEnabled;
        [SerializeField] private bool _isPausePanel;
        [SerializeField] private Button[] _closeButtons;
        [SerializeField] private Button[] _openButtons;
        [SerializeField] private Animator _animator;
        [SerializeField] private CastZone _castZone;

        public event Action<Panel> Enabled;
        public event Action<Panel> Disabled;
        
        private const string PanelEnabled = "PanelEnabled";
        private const string PanelDisabled = "PanelDisabled";
        private const string PanelShowIdle = "PanelIdle";
        private const string PanelHideIdle = "PanelHideIdle";

        public bool IsEnabled => _isEnabled;
        public bool IsInGamePanel => _isInGamePanel;

        private void OnEnable()
        {
            if (_closeButtons != null)
            {
                foreach (Button button in _closeButtons)
                    button.onClick.AddListener(TurnOff);
            }

            if (_openButtons != null)
            {
                foreach (Button button in _openButtons)
                    button.onClick.AddListener(TurnOn);
            }
        }

        private void OnDisable()
        {
            if (_closeButtons != null)
            {
                foreach (Button button in _closeButtons)
                    button.onClick.RemoveListener(TurnOff);
            }

            if (_openButtons != null)
            {
                foreach (Button button in _openButtons)
                    button.onClick.RemoveListener(TurnOn);
            }
        }

        public void TurnOn()
        {
            ShowCanvas();
            Enabled?.Invoke(this);
        }

        public void TurnOnWithoutInvoke()
        {
            ShowCanvas();
        }

        public void TurnOff()
        {
            HideCanvas();
            Disabled?.Invoke(this);
        }

        public void TurnOffWithoutInvoke()
        {
            HideCanvas();
        }

        private void ShowCanvas()
        {
            _isEnabled = true;
            
            _animator.Play(PanelEnabled);

            if (_isPausePanel)
            {
                _castZone.gameObject.SetActive(false);
                Time.timeScale = 0;
            }
        }

        public void ShowCanvasSwitcher()
        {
            _isEnabled = true;
            
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
        
        private void HideCanvas()
        {
            _isEnabled = false;
            
            _animator.Play(PanelDisabled);
        }

        public void HideCanvasSwitcher()
        {
            _isEnabled = false;
            
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            if (_isPausePanel)
            {
                _castZone.gameObject.SetActive(true);
                Time.timeScale = 1;
            }
        }
        
        private void OnShowed()
        {
            _animator.Play(PanelShowIdle);
        }

        private void OnHide()
        {
            _animator.Play(PanelHideIdle);

            if (_isPausePanel)
            {
                _castZone.gameObject.SetActive(true);
                Time.timeScale = 1;
            }
        }
    }
}
