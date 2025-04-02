using System;
using MainScene.Bird;
using UnityEngine;

namespace MainScene.ThroughPipe
{
    public class ThroughPipeCollider : MonoBehaviour
    {
        public Action ThroughPipeColliderAction;
        
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<BirdMark>(out BirdMark _))
            {
                ThroughPipeColliderAction?.Invoke();
            }
        }
    }   
}