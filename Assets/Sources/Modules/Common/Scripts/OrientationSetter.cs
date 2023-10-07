using UnityEngine;

namespace Sources.Modules.Common.Scripts
{
    public class OrientationSetter : MonoBehaviour
    {
        [SerializeField] private OrientationType _orientation;
        
        public enum OrientationType
        {
            Portrait,
            Landscape,
        }

        private void Start()
        {
            SwapOrientation(_orientation);
        }

        public void SwapOrientation(OrientationType orientationType)
        {
            switch (orientationType)
            {
                case OrientationType.Landscape:
                    Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;

                    Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                    Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                    break;
                
                case OrientationType.Portrait:
                    Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;
                    Screen.orientation = UnityEngine.ScreenOrientation.Portrait;

                    Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
                    Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                    break;
            }
        }
    }
}
