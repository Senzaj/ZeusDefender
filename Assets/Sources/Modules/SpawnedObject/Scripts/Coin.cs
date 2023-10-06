using System;
using Sources.Modules.Player.Scripts;
using UnityEngine;

namespace Sources.Modules.SpawnedObject.Scripts
{
    public class Coin : CommonObject
    {
        [SerializeField] private int _value;
        [SerializeField] private ParticleSystem _boom;

        public Action<Coin, int> Taken;
        public Action<Coin> Skipped;

        private void OnEnable()
        {
            StartMovement();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerSkin _))
            {
                ParticleSystem boom = Instantiate(_boom);
                boom.transform.position = transform.position;
                Taken?.Invoke(this, _value);
                gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            StopMovement();
            Skipped?.Invoke(this);
        }
    }
}
