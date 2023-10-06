using System;
using Sources.Modules.Player.Scripts;
using UnityEngine;

namespace Sources.Modules.SpawnedObject.Scripts
{
    public class Obstacle : CommonObject
    {
        [SerializeField] private ParticleSystem _boom;

        public event Action<Obstacle> PlayerDefeated;
        public event Action<Obstacle> Skipped;

        private void OnEnable()
        {
            StartMovement();
        }

        private void OnDisable()
        {
            StopMovement();
            Skipped?.Invoke(this);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerSkin player))
            {
                ParticleSystem boom = Instantiate(_boom);
                boom.transform.position = transform.position;
                player.OnDeath();
                PlayerDefeated?.Invoke(this);
            }
        }
    }
}
