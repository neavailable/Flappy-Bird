using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MainScene.UI
{
    public class StoreButton : MonoBehaviour
    {
        public Action StoreOpened;
        
        [SerializeField] private GameObject _store;
        [SerializeField] private List<GameObject> _hiddeObjects;
        private bool _shouldShowStore;


        private void Start()
        {
            _shouldShowStore = false;
        }

        public void ShowOrHideStore()
        {
            _shouldShowStore = !_shouldShowStore;
            
            _store.SetActive(_shouldShowStore);
            if (_shouldShowStore) StoreOpened?.Invoke();
            foreach (GameObject hiddeObject in _hiddeObjects)
            {
                hiddeObject.SetActive(!_shouldShowStore);
            }

            Time.timeScale = _shouldShowStore ? 0f : 1f;
        }
    }
}