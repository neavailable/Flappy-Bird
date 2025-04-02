using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneAsset _main;
        [SerializeField] private SceneAsset _UI;
        private MainSceneButton _mainSceneButton;
        private UISceneButton _UISceneButton;


        public void SetMainSceneButton(MainSceneButton mainSceneButton)
        {
            _mainSceneButton = mainSceneButton;
            
            _mainSceneButton.ButtonPressed += LoadMainScene;
        }
        
        public void SetUISceneButton(UISceneButton uiSceneButton)
        {
            _UISceneButton = uiSceneButton;
            
            _UISceneButton.ButtonPressed += LoadUIScene;
        }
        
        private void Awake() => DontDestroyOnLoad(gameObject);
        
        private void Start() => LoadUIScene();
        
        private void LoadScene(SceneAsset scene)
        {
            SceneManager.LoadScene(scene.name);
            
            Time.timeScale = 1f;
        }
        
        private void LoadMainScene() => LoadScene(_main);
        
        private void LoadUIScene() => LoadScene(_UI);

        private void OnDestroy()
        {
            if (_mainSceneButton) _mainSceneButton.ButtonPressed -= LoadMainScene;
            if (_UISceneButton) _UISceneButton  .ButtonPressed -= LoadUIScene;
        }
    }
}