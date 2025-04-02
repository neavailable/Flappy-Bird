using MainScene.Bird;
using System;
using UnityEngine;

namespace MainScene.Ground
{
    public class BirdDestroyer : MonoBehaviour
    {
        public Action BirdCollided;


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<BirdMark>
            (
                out BirdMark _
            ))
            {
                BirdCollided?.Invoke();   
            }
        }
    }
}
