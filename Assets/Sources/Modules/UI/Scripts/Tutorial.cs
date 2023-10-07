using UnityEngine.UI;
using UnityEngine;
using System;

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
            GetComponent<Panel>().HideCanvasForSwitcher();
            _nextButton.onClick.AddListener(TryShowNextSlide);
            OnEnabled?.Invoke();
        }

        private void OnDisable()=>
            _nextButton.onClick.RemoveListener(TryShowNextSlide);

        public void Begin()
        {
            GetComponent<Panel>().TurnOnWithoutInvoke();
            TryShowNextSlide();
        }

        public void OnTutorialPassed() =>
            IsTutorialPassed = true;

        private void TryShowNextSlide()
        {
            if (_currentSlideIndex < _slideCanvases.Length)
            {
                HideAllCanvases();
                Show(_slideCanvases[_currentSlideIndex]);
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
                Hide(canvas);
        }

        private void Show(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }

        private void Hide(CanvasGroup canvasGroup)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
        }
    }
}
