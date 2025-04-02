using System;
using UnityEngine;

namespace MainScene.Bird
{
    public class BirdDying : MonoBehaviour
    {
        public Action StartedDying, Dying, SelfDestroy;
        
        [SerializeField] private Ground.BirdDestroyer _bottomBorder;
        private bool _startDyingAnimation;


        public void Constructor(Ground.BirdDestroyer bottomBorder)
        {
            _bottomBorder = bottomBorder;
            _bottomBorder.BirdCollided += SelfDestroying;
        }

        public void StartDying() 
        {
            if (_startDyingAnimation) return;
            
            _startDyingAnimation = true;
            StartedDying?.Invoke();
        }

        private void Start() => _startDyingAnimation = false;

        private void SelfDestroying()
        {
            SelfDestroy?.Invoke();

            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (_startDyingAnimation) Dying?.Invoke();
        }

        private void OnDestroy()
        {
            _bottomBorder.BirdCollided -= SelfDestroying;
        }
    }
}
