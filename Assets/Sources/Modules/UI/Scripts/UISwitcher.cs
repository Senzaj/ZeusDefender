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
            _orientationSetter.SwapOrientation(OrientationSetter.OrientationType.Portrait);
            
            foreach (Panel panel in _panels)
            {
                if (panel.IsEnabled)
                    panel.TurnOnWithoutInvoke();
                else
                    panel.HideCanvasForSwitcher();
            }
        }
    }
}
