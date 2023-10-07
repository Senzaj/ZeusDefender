using System;
using Sources.Modules.Player.Scripts.Weapon;
using UnityEngine;

namespace Sources.Modules.CommonMovingObject.Scripts
{
    public class Obstacle : CommonObject
    {
        [SerializeField] private ParticleSystem _boom;

        public event Action<Obstacle> PlayerDefeated;
        public event Action<Obstacle> Skipped;

        private void OnDisable()
        {
            StopMovement();
            Skipped?.Invoke(this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerWeapon player))
            {
                ParticleSystem boom = Instantiate(_boom);
                boom.transform.position = transform.position;
                player.OnExploded();
                PlayerDefeated?.Invoke(this);
                gameObject.SetActive(false);
            }
        }
    }
}
