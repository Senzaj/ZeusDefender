using UnityEngine;
using System.Collections.Generic;
using Sources.Modules.CommonMovingObject.Scripts;

namespace Sources.Modules.ObjectFactory.Scripts
{
    public class ObjectsFactory : MonoBehaviour
    {
        [SerializeField] private int _startCapacity;
        [SerializeField] private CommonObject _prefabForSpawn;

        private List<CommonObject> _objects;
        
        public void Init()
        {
            _objects = new List<CommonObject>();
            
            for (int i = 0; i < _startCapacity; i++)
                InitObject();
        }

        public CommonObject GetObject()
        {
            CommonObject inactiveObj = null;

            foreach (CommonObject obj in _objects)
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

        private CommonObject InitObject()
        {
            CommonObject newObject = Instantiate(_prefabForSpawn, transform);
            newObject.gameObject.SetActive(false);
            _objects.Add(newObject);
            return newObject;
        }
    }
}
