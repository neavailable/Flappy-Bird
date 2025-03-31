using System;
using UnityEngine;
using MainScene.Bird;

namespace MainScene.UserInput
{
    [RequireComponent(typeof(BirdDying))]
    public class BirdInput : MonoBehaviour
    {
        public Action<bool> SpaceClicked;

        private BirdDying _birdDying;
        private bool _canProvideInput;


        private void OnEnable()
        {
            _birdDying = GetComponent<BirdDying>();
            _birdDying.StartedDying += BlockInput;
        }

        private void Start() => _canProvideInput = true; 

        private void BlockInput() => _canProvideInput = false;
        
        private void EachFrameInput()
        {
            if (Input.GetKeyDown(KeyCode.Space)) SpaceClicked?.Invoke(_canProvideInput);
        }
        
        private void Update() => EachFrameInput();
        
        private void OnDisable()
        {
            _birdDying.StartedDying -= BlockInput;
        }
    }	
}