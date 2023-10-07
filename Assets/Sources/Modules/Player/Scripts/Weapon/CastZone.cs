using UnityEngine;

namespace Sources.Modules.Player.Scripts.Weapon
{
    public class CastZone : MonoBehaviour
    {
        [SerializeField] private PreSpawnZone _spawnZone;

        private bool _isSelectingDir;

        private void OnEnable()
        {
            _spawnZone.Launched += OnLaunched;
        }

        private void OnDisable()
        {
            _spawnZone.Launched -= OnLaunched;
        }
        
        private void OnLaunched() => _isSelectingDir = false;
        
        public void OnMouseDown()
        {
            if (_isSelectingDir == false)
            {
                _isSelectingDir = true;
                Vector3 touchPosV3 = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
                Vector2 touchPos = new Vector2(touchPosV3.x, touchPosV3.y);
                _spawnZone.Init(touchPos);
            }
        }
    }
}
