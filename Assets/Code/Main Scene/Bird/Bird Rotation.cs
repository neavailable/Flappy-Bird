using UnityEngine;

namespace MainScene.Bird
{
    [RequireComponent(typeof(BirdMovement), typeof(BirdDying))]
    public class BirdRotation : MonoBehaviour
    {
        [Range(2f, 10f), SerializeField] private float _speed;
        [SerializeField] private Quaternion _targetUp;
        private BirdMovement _birdMovement;
        private BirdDying _birdDying;
        private int _currentDirection;
        private bool _shouldRotate;


        private void OnEnable()
        {
            _birdMovement = GetComponent<BirdMovement>();
            _birdDying = GetComponent<BirdDying>();

            _birdMovement.RotateAction += Rotate;
            _birdDying   .Dying        += StopRotate;
        }

        private void Start()
        {
            _currentDirection = 1;
            _shouldRotate = true;
        }
                
        private void StopRotate()
        {
            _shouldRotate = false;
            transform.rotation = Quaternion.identity; 
        }

        private void Rotate(int direction)  
        {   
            if (!_shouldRotate) return;


            if (_currentDirection != direction)
            {
                _targetUp.z = -_targetUp.z;
                _currentDirection = direction;
            }
                    
            transform.rotation = Quaternion.Slerp
            (
                transform.rotation,
                _targetUp,
                _speed * Time.fixedDeltaTime
            );
        }

        private void OnDisable() 
        {
            _birdMovement.RotateAction -= Rotate;
            _birdDying   .Dying        -= StopRotate;
        }
    }	
}