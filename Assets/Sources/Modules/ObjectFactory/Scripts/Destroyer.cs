using Sources.Modules.CommonMovingObject.Scripts;
using UnityEngine;

namespace Sources.Modules.ObjectFactory.Scripts
{
    public class Destroyer : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CommonObject obj))
                obj.gameObject.SetActive(false);
        }
    }
}
