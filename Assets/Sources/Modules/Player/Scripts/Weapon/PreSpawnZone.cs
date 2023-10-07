using System;
using UnityEngine;

namespace Sources.Modules.Player.Scripts.Weapon
{
    public class PreSpawnZone : MonoBehaviour
    {
        [SerializeField] private WeaponsFactory _weaponsFactory;
        [SerializeField] private Vector2 _disabledPosition;
        [SerializeField] private ParticleSystem _castFX;

        public event Action Launched;
        
        public void Init(Vector2 startPosition)
        {
            transform.position = startPosition;
        }
        
        public void OnMouseDown()
        {
            ThrowWeapon();
        }

        private void ThrowWeapon()
        {
            PlayerWeapon newPlayerWeapon = _weaponsFactory.GetObject();
            newPlayerWeapon.gameObject.SetActive(true);
            
            Vector3 touchPosV3 = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            newPlayerWeapon.transform.position = transform.position;
            newPlayerWeapon.MoveToTarget(new Vector2(touchPosV3.x, touchPosV3.y));
            
            ParticleSystem boom = Instantiate(_castFX);
            boom.transform.position = newPlayerWeapon.transform.position;

            transform.position = _disabledPosition;
            Launched?.Invoke();
        }
    }
}
