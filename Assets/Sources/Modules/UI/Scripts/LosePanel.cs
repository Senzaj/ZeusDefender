using Sources.Modules.Wallet.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class LosePanel : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private ScoreCounter _scoreCounter;

        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OpenMenu);
            _restartButton.onClick.AddListener(ReloadScene);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OpenMenu);
            _restartButton.onClick.RemoveListener(ReloadScene);
        }

        public void SetScore()
        {
            _score.text = _scoreCounter.FixScore().ToString();
        }

        private void OpenMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
        
        private void ReloadScene()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
