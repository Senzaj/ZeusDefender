using UnityEngine;

namespace Sources.Modules.Common.Scripts
{
    public class OrientationSetter : MonoBehaviour
    {
        public enum OrientationType
        {
            Portrait,
            Landscape,
        }

        [SerializeField] private OrientationType _orientation;
        
        private void Start()
        {
            ChangeOrientation(_orientation);
        }

        public void ChangeOrientation(OrientationType orientationType)
        {
            switch (orientationType)
            {
                case OrientationType.Portrait:
                    Screen.orientation = UnityEngine.ScreenOrientation.Portrait;
                    Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;

                    Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = true;
                    Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = false;
                    break;

                case OrientationType.Landscape:
                    Screen.orientation = UnityEngine.ScreenOrientation.LandscapeLeft;
                    Screen.orientation = UnityEngine.ScreenOrientation.AutoRotation;

                    Screen.autorotateToPortrait = Screen.autorotateToPortraitUpsideDown = false;
                    Screen.autorotateToLandscapeLeft = Screen.autorotateToLandscapeRight = true;
                    break;
            }
        }
    }
}
