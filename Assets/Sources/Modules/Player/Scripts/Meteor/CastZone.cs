using UnityEngine;

namespace Sources.Modules.Player.Scripts.Meteor
{
    public class CastZone : MonoBehaviour
    {
        [SerializeField] private UFO _ufo;
        [SerializeField] private MeteorFactory _meteorFactory;
        [SerializeField] private ParticleSystem _castFX;

        public void OnMouseDown()
        {
            Meteor newMeteor = _meteorFactory.GetObject();
            newMeteor.gameObject.SetActive(true);
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.touches[0].position);
            newMeteor.transform.position = new Vector2(mousePos.x, mousePos.y);
            ParticleSystem boom = Instantiate(_castFX);
            boom.transform.position = newMeteor.transform.position;
            newMeteor.MoveToTarget(_ufo.transform);
        }
    }
}
