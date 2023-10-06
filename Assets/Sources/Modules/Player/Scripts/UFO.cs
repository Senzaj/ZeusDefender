using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    public class UFO : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rb;

        public void SetVelocity(Vector2 _vector2, float pushPower)
        {
            _rb.AddForce(_vector2.normalized * pushPower, ForceMode2D.Impulse);
        }
    }
}
