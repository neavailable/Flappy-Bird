using UnityEngine;

namespace MainScene.Object
{
    public class ObjectMovement : MonoBehaviour
    {
        [Range(0f, 10f), SerializeField] private float _speed;
        private Bird.BirdDying _birdDying;


        public void Constructor(Bird.BirdDying birdDying)
        {
            _birdDying = birdDying;

            _birdDying.SelfDestroy += StopPipe;
        }
                
        private void StopPipe() 
        {
            _speed = 0f;
        } 

        private void Move()
        {
            transform.position += Vector3.left * _speed * Time.fixedDeltaTime;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDestroy()
        {
            if (_birdDying) _birdDying.SelfDestroy -= StopPipe;
        }
    }
}