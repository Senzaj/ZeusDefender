using System.Collections.Generic;
using UnityEngine;

namespace Sources.Modules.Player.Scripts.Meteor
{
    public class MeteorFactory : MonoBehaviour
    {
        [SerializeField] private Meteor _prefab;
        [SerializeField] private int _startCapacity;
    
        private List<Meteor> _objects;
        
        public void Awake()
        {
            _objects = new List<Meteor>();
                
            for (int i = 0; i < _startCapacity; i++)
                InitObject();
        }
    
        public Meteor GetObject()
        {
            Meteor inactiveObj = null;
    
            foreach (Meteor obj in _objects)
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
    
        private Meteor InitObject()
        {
            Meteor newObject = Instantiate(_prefab, transform);
            newObject.gameObject.SetActive(false);
            _objects.Add(newObject);
            return newObject;
        }
    }
}
