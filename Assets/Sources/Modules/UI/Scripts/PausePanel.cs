using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Modules.UI.Scripts
{
    public class PausePanel : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        private void OnEnable() => _exitButton.onClick.AddListener(LoadMenu);

        private void OnDisable() => _exitButton.onClick.RemoveListener(LoadMenu);
        
        private void LoadMenu() =>
            SceneManager.LoadScene("MainMenu");
    }
}
