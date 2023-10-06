using Sources.Modules.Common.Scripts;
using UnityEngine;

namespace Sources.Modules.UI.Scripts
{
    public class UISwitcher : MonoBehaviour
    {
        [SerializeField] private Panel[] _panels;
        [SerializeField] private OrientationSetter _orientationSetter;

        public void Init()
        {   
            _orientationSetter.ChangeOrientation(OrientationSetter.OrientationType.Portrait);
            
            foreach (Panel panel in _panels)
            {
                panel.Enabled += OnPanelEnabled;
                panel.Disabled += OnPanelDisabled;

                if (panel.IsEnabled)
                    panel.TurnOnWithoutInvoke();
                else
                    panel.HideCanvasSwitcher();
            }
        }

        private void OnDisable()
        {
            foreach (Panel panel in _panels)
            {
                panel.Enabled -= OnPanelEnabled;
                panel.Disabled -= OnPanelDisabled;
            }
        }

        private void OnPlayerLose()
        {
            foreach (Panel panel in _panels)
            {
                if (panel.IsInGamePanel)
                {
                    panel.TurnOffWithoutInvoke();
                    break;
                }
            }
        }
        
        private void OnPanelEnabled(Panel activatedPanel)
        {
            foreach (Panel panel in _panels)
            {
                if (panel.IsEnabled && panel != activatedPanel)
                    panel.TurnOffWithoutInvoke();
            }
        }
        
        private void OnPanelDisabled(Panel deactivatedPanel)
        {
            
        }
    }
}
