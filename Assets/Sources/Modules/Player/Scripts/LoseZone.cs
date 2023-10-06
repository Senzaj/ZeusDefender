using System;
using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    public class LoseZone : MonoBehaviour
    {
        public event Action OnLoseZoneEntered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out PlayerSkin player))
            {
                OnLoseZoneEntered?.Invoke();
            }
        }
    }
}
