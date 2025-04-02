using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainScene.WindowOfLoose
{
    public class WindowOfLooseShower : MonoBehaviour
    {
        public Action SendScore;
        
        private Bird.BirdDying _birdDying;
        private GameObject _windowOfLoose;
        private List<GameObject> _objectsToHide;


        public void Constructor
        (
            Bird.BirdDying birdDying,
            GameObject windowOfLoose,
            List<GameObject> objectsToHide
        )
        {
            _birdDying = birdDying;
            _windowOfLoose = windowOfLoose;
            _objectsToHide = objectsToHide;
            
            _birdDying.SelfDestroy += ShowWindowOfLoose;
        }

        private void ShowWindowOfLoose()
        {
            _windowOfLoose.SetActive(true);
            SendScore?.Invoke();
            foreach (GameObject objectToHide in _objectsToHide) objectToHide.SetActive(false);
            
            Time.timeScale = 0f;
        }

        private void OnDestroy()
        {
            _birdDying.SelfDestroy -= ShowWindowOfLoose;
        }
    }
}