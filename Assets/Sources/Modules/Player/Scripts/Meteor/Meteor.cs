using UnityEngine;

namespace Sources.Modules.Player.Scripts.Meteor
{
    public class Meteor : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;
        [SerializeField] private ParticleSystem _deathFx;

        private Vector2 targetVelocity;
        
        public void MoveToTarget(Transform target)
        {
            _rigidbody2D.velocity = Vector2.zero;
            targetVelocity = target.position - transform.position;
            _rigidbody2D.velocity = targetVelocity.normalized * _speed;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out UFO ufo) || other.gameObject.TryGetComponent(out Meteor _))
            {
                ParticleSystem boom = Instantiate(_deathFx);
                boom.transform.position = transform.position;
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Boarder _))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
