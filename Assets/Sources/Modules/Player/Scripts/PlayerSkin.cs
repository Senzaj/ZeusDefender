using System;
using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ParticleSystem _deathFx;

        private bool _isFireShown;

        public void ChangeSkin(Sprite newColor)
        {
            _spriteRenderer.sprite = newColor;
        }

        public void OnDeath()
        {
            ParticleSystem fx = Instantiate(_deathFx);
            fx.transform.position = transform.position;
            gameObject.SetActive(false);
        }
    }
}
