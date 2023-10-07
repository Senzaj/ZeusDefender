using System;
using Sources.Modules.Player.Scripts.Weapon;
using UnityEngine;

namespace Sources.Modules.CommonMovingObject.Scripts
{
    public class Coin : CommonObject
    {
        [SerializeField] private int _value;
        [SerializeField] private ParticleSystem _boom;

        public Action<Coin, int> Taken;
        public Action<Coin> Skipped;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerWeapon weapon))
            {
                ParticleSystem boom = Instantiate(_boom);
                boom.transform.position = transform.position;
                weapon.OnExploded();
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
