using System;
using UnityEngine;

namespace MainScene.Bird
{
    [RequireComponent(typeof(MainScene.UserInput.BirdInput), typeof(BirdDying))]
    public class BirdMovement : MonoBehaviour
    {        
        public Action<int> RotateAction;

        [Header("Gravity Settings")]
        [Range(0f, 10f), SerializeField] private float _startDownForce;
        [Range(0f, 15), SerializeField] private float _acceleration;
        [Header("Push Up Settings")]
        [Range(0f, 20f), SerializeField] private float _pushForce; 
        [Range(0.1f, 0.3f), SerializeField] private float _pushTime;
        private const float _maxDownForce = 20;
        private MainScene.UserInput.BirdInput _birdInput;
        private BirdDying _birdDying;
        private float _currentDownForce, _currentTime;
        private int _direction;
        private bool _shouldPush, _destroyingNow;


        private void SetShouldPush()
        {
            _shouldPush = Time.time - _currentTime < _pushTime;
        }

        private void OnEnable()
        {
            _birdInput = GetComponent<MainScene.UserInput.BirdInput>();
            _birdDying = GetComponent<BirdDying>();

            _birdInput.SpaceClicked += StartPushing;
            _birdDying.StartedDying += DestroyingNow;
            _birdDying.Dying        += DownForceEffect;
        }

        private void Start()
        {            
            // ставлю -_pushTime, типу для того аби з самого початку гри
            // пташка не летіла вгору
            _currentTime = -_pushTime;

            _direction = -1;
            _destroyingNow = false;
        }

        private void DestroyingNow() => _destroyingNow = true;

        private void StartPushing(bool shouldPush)
        {
            _shouldPush = shouldPush;
            if (!_shouldPush) return;
            
            _currentTime = Time.time;

            _currentDownForce = _startDownForce;
            _direction = 1;
        }

        private void Push()
        {
            transform.position += 
                Vector3.up * _pushForce * Time.fixedDeltaTime;
        }

        private void DownForceEffect()
        {
            if (_currentDownForce < _maxDownForce) 
            {
                _currentDownForce += _acceleration * Time.fixedDeltaTime;
            }

            float timedDownForcePower = _currentDownForce * Time.fixedDeltaTime;
            transform.position += Vector3.down * timedDownForcePower;

            _direction = -1;
        }

        private void Move()
        {
            if (_destroyingNow) return;


            SetShouldPush();
            if (_shouldPush) Push();

            else DownForceEffect();

            RotateAction?.Invoke(_direction);
        }

        private void FixedUpdate() 
        {
            Move();
        }

        private void OnDisable()
        {
            _birdInput.SpaceClicked -= StartPushing;
            _birdDying.StartedDying -= DestroyingNow;
            _birdDying.Dying        -= DownForceEffect;
        }
    }	
}