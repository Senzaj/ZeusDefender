using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Player.Scripts.Weapon
{
    public class WeaponsFactory : MonoBehaviour
    {
        [SerializeField] private PlayerWeapon _prefab;
        [SerializeField] private int _startCapacity;
    
        private List<PlayerWeapon> _objects;
        
        public void Awake()
        {
            _objects = new List<PlayerWeapon>();
            for (int i = 0; i < _startCapacity; i++)
                InitObject();
        }

        public void SetWeapon(PlayerWeapon prefab) => _prefab = prefab;
        
    
        public PlayerWeapon GetObject()
        {
            PlayerWeapon inactiveObj = null;
    
            foreach (PlayerWeapon obj in _objects)
            {
                if (obj.isActiveAndEnabled == false)
                {
                    inactiveObj = obj;
                    break;
                }
            }
    
            if (inactiveObj == null)
                inactiveObj = InitObject();
    
            return inactiveObj;
        }
    
        private PlayerWeapon InitObject()
        {
            PlayerWeapon newObject = Instantiate(_prefab, transform);
            newObject.gameObject.SetActive(false);
            _objects.Add(newObject);
            return newObject;
        }
    }
}
