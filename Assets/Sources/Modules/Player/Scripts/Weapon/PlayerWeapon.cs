using UnityEngine;

namespace Sources.Modules.Player.Scripts.Weapon
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;
        [SerializeField] private ParticleSystem _deathFx;

        private bool _isFirstInst = true;
        private Vector2 targetVelocity;
        
        public void MoveToTarget(Vector3 target)
        {
            _rigidbody2D.velocity = Vector2.zero;
            targetVelocity = target - transform.position;
            _rigidbody2D.velocity = targetVelocity.normalized * _speed;
        }
        
        public void OnExploded()
        {
            ParticleSystem boom = Instantiate(_deathFx);
            boom.transform.position = transform.position;
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            if (_isFirstInst)
            {
                _isFirstInst = false;
            }
            else
            {
                ParticleSystem boom = Instantiate(_deathFx);
                boom.transform.position = transform.position;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Boarder _))
            {
                ParticleSystem boom = Instantiate(_deathFx);
                boom.transform.position = transform.position;
                gameObject.SetActive(false);
            }
        }
    }
}
