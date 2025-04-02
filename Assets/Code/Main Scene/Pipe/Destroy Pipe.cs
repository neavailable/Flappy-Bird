using MainScene.Object;
using System;
using UnityEngine;

namespace MainScene.Pipe
{
    [RequireComponent(typeof(ObjectCollider))]
    public class DestroyPipe : MonoBehaviour
    {
        public Action<GameObject> SelfDestroyAction;
        private ObjectCollider _objectCollider;
            
            
        private void OnEnable()
        {
            _objectCollider = GetComponent<ObjectCollider>();
            _objectCollider.SelfDestroyAction += SelfDestroy;
        }

        private void SelfDestroy()
        {
            SelfDestroyAction?.Invoke(transform.root.gameObject);
            Destroy(transform.root.gameObject);
        }
        
        private void OnDisable()
        {
            _objectCollider.SelfDestroyAction -= SelfDestroy;
        }
    }
}