using System;
using UnityEngine;

namespace MainScene.Ground
{
    public class GroundCreatorCollider : MonoBehaviour
    {
        public Action OnGroundCreate;


        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<GroundMark>
            (
                out GroundMark _
            ))
            {
                OnGroundCreate?.Invoke();
            }
        }
    }
}
