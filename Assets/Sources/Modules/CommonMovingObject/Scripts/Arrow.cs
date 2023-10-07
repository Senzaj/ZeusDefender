using System;
using Sources.Modules.Player.Scripts.Weapon;
using UnityEngine;

namespace Sources.Modules.CommonMovingObject.Scripts
{
    public class Arrow : CommonObject
    {
        [SerializeField] private ParticleSystem _boom;
        
        public event Action<Arrow> PlayerDefeated;
        public event Action<Arrow> Exploded;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerWeapon weapon))
            {
                ParticleSystem boom = Instantiate(_boom);
                weapon.OnExploded();
                boom.transform.position = transform.position;
                Exploded?.Invoke(this);
                gameObject.SetActive(false);
            }
            
            if (other.TryGetComponent(out LoseZone _))
            {
                ParticleSystem boom = Instantiate(_boom);
                boom.transform.position = transform.position;
                PlayerDefeated?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
