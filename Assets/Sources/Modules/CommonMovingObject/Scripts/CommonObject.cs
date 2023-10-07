using System.Collections;
using UnityEngine;

namespace Sources.Modules.CommonMovingObject.Scripts
{
    public class CommonObject : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected float _speed;

        private Vector2 _direction;
        private Coroutine _movementWork;
        private bool _canMove;

        public void StartMovement(Vector2 dir)
        {
            _direction = dir;
            _canMove = true;
            _movementWork = StartCoroutine(Moving());
        }

        protected void StopMovement()
        {
            if (_movementWork != null)
                StopCoroutine(_movementWork);

            _canMove = false;
        }
        
        private IEnumerator Moving()
        {
            while (_canMove)
            {
                _rigidbody.velocity = _direction * _speed;
                yield return null;
            }
        }
    }
}
