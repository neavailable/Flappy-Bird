using Scenes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BootstrapScene
{
    public class UISceneFactory : MonoBehaviour
    {
        [SerializeField] private SceneAsset _uiScene;
        [SerializeField] private GameObject _UIPrefab, _eventSystemPrefab;
        [SerializeField] private SceneLoader _sceneLoader;
        
        
        private void Awake() => DontDestroyOnLoad(gameObject);
        
        private void OnEnable() => SceneManager.sceneLoaded += InstantiateAll;
        
        private MainSceneButton InstantiateUI()
        {
            return Instantiate(_UIPrefab).GetComponentInChildren<MainSceneButton>();
        }
        
        private void InstantiateEventSystem() => Instantiate(_eventSystemPrefab);
        
        private void InstantiateAll(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != _uiScene.name) return;

            InstantiateEventSystem();
            _sceneLoader.SetMainSceneButton(InstantiateUI());
        }

        private void OnDisable() => SceneManager.sceneLoaded -= InstantiateAll;
    }
}