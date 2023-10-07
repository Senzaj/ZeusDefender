using Sources.Modules.Player.Scripts.Weapon;
using UnityEngine;

namespace Sources.Modules.Player.Scripts
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private WeaponsFactory _weaponsFactory; 

        private bool _isFireShown;

        public void ChangeWeapon(PlayerWeapon newWeapon) => _weaponsFactory.SetWeapon(newWeapon);
    }
}
