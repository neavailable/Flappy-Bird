using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneAsset _scenes;


        public void LoadScene()
        {
            SceneManager.LoadScene(_scenes.name);
            
            Time.timeScale = 1f;
        }
    }
}