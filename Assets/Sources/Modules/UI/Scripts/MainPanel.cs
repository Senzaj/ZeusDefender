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
            _playButton.onClick.AddListener(StartGame);
            _exitButton.onClick.AddListener(ExitGame);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(StartGame);
            _exitButton.onClick.RemoveListener(ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
        
        private void StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}
