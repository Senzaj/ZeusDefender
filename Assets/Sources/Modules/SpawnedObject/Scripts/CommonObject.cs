using System.Collections;
using UnityEngine;

namespace Sources.Modules.SpawnedObject.Scripts
{
    public class CommonObject : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody;
        [SerializeField] protected float _speed;

        private Coroutine _movementWork;
        private bool _canMove;

        protected void StartMovement()
        {
            _canMove = true;
            _movementWork = StartCoroutine(MovingDown());
        }

        protected void StopMovement()
        {
            if (_movementWork != null)
                StopCoroutine(_movementWork);

            _canMove = false;
        }
        
        private IEnumerator MovingDown()
        {
            while (_canMove)
            {
                _rigidbody.velocity = Vector2.down * _speed;
                yield return null;
            }
        }
    }
}
