using System;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class Tutorial : MonoBehaviour
    {
        [SerializeField] private CanvasGroup[] _slideCanvases;
        [SerializeField] private Button _nextButton;
        
        public event Action TutorialPassed;
        public event Action OnEnabled;

        private int _currentSlideIndex;
        
        public bool IsTutorialPassed { get; private set; }

        public void Init()
        {
            GetComponent<Panel>().HideCanvasSwitcher();
            _nextButton.onClick.AddListener(SwitchSlide);
            OnEnabled?.Invoke();
        }

        private void OnDisable()
        {
            _nextButton.onClick.RemoveListener(SwitchSlide);
        }

        public void Begin()
        {
            GetComponent<Panel>().TurnOnWithoutInvoke();
            SwitchSlide();
        }

        public void OnTutorialPassed()
        {
            IsTutorialPassed = true;
        }
        
        private void SwitchSlide()
        {
            if (_currentSlideIndex < _slideCanvases.Length)
            {
                HideAllCanvases();
                ShowCanvas(_slideCanvases[_currentSlideIndex]);
                _currentSlideIndex++;
            }
            else
            {
                GetComponent<Panel>().TurnOffWithoutInvoke();
                IsTutorialPassed = true;
                TutorialPassed?.Invoke();
            }
        }

        private void HideAllCanvases()
        {
            foreach (CanvasGroup canvas in _slideCanvases)
                HideCanvas(canvas);
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
