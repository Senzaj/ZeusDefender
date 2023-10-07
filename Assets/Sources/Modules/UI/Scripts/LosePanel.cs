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
        [SerializeField] private ScoreMeter _scoreCounter;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(ReloadScene);
            _exitButton.onClick.AddListener(OpenMenu);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(ReloadScene);
            _exitButton.onClick.RemoveListener(OpenMenu);
        }

        public void SetScore() => _score.text = _scoreCounter.FixScore().ToString();

        private void OpenMenu() => SceneManager.LoadScene("MainMenu");

        private void ReloadScene() => SceneManager.LoadScene("GameScene");
    }
}
