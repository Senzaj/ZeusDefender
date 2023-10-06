using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        
        private void OnEnable()
        {
            _exitButton.onClick.AddListener(OpenMenu);
        }

        private void OnDisable()
        {
            _exitButton.onClick.RemoveListener(OpenMenu);
        }
        
        private void OpenMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
