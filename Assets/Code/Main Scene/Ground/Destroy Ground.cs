using UnityEngine;
using System;

namespace MainScene.Ground
{
    public class DestroyGround : MonoBehaviour
    {
        public Action SelfDestroyed;


        public void SelfDestroy()
        {
            Destroy(gameObject);

            SelfDestroyed?.Invoke();
        }
    }
}