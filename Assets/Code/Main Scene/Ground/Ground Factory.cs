using System;
using MainScene.Object;
using UnityEngine;

namespace MainScene.Ground
{
    [RequireComponent(typeof(GroundCreatorCollider))]
    public class GroundFactory : MonoBehaviour
    {
        public Func<Vector2> LocateGround;

        [SerializeField] private GameObject _groundPrefab;
        private Bird.BirdDying _birdDying;
        
        private GroundCreatorCollider _groundCreatorCollider;

        
        public void Constructor(Bird.BirdDying birdDying)
        {
            _birdDying = birdDying;
        }

        
        private void OnEnable()
        {
            _groundCreatorCollider = GetComponent<GroundCreatorCollider>();

            _groundCreatorCollider.OnGroundCreate += InstantiateGround;
        }
        
        private void Start() => BindGround(Instantiate(_groundPrefab));

        private void BindGround(GameObject ground)
        {   
            ground.GetComponent<ObjectMovement>().Constructor(_birdDying);
        }

        private void InstantiateGround()
        {
            BindGround(Instantiate
            (
                _groundPrefab,
                LocateGround.Invoke(),
                Quaternion.identity
            ));
        }

        private void OnDisable()
        {
            _groundCreatorCollider.OnGroundCreate -= InstantiateGround;
        }
    }
}