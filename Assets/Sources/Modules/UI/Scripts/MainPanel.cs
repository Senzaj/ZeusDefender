using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class MainPanel : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        private void OnEnable()
        {
            _playButton.onClick.AddListener(PlayGame);
            _exitButton.onClick.AddListener(ExitApp);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(ExitApp);
            _playButton.onClick.RemoveListener(PlayGame);
        }

        private void ExitApp() => Application.Quit();

        private void PlayGame() => SceneManager.LoadScene("GameScene");
    }
}
