using System;
using System.Collections.Generic;
using MainScene.Bird;
using MainScene.Store;
using MainScene.Ground;
using MainScene.Pipe;
using MainScene.UI;
using MainScene.WindowOfLoose;
using Scenes;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace BootstrapScene
{
    public class MainSceneFactory : MonoBehaviour
    {
        public Action<GameObject, BirdDestroyer, Store> BindBird;
        public Action<GameObject, BirdDying> BindGround;
        public Action<GameObject, BirdDying, Scores, Store> BindPipesFactory;
        public Action<AllScoresText, CurrentScore> BindStore;
        public Action<GameObject, Store, Scores> BindCurrentScoreAction;
        public Action<Scores, PipesFactory> BindPipesFactoryToStandardUI;  
        public Action<Scores, WindowOfLooseShower, StoreButton, GameObject> BindStandardUI;  
        public Action<ScoreText, Scores> BindWindowOfLoose;        
        public Action<GameObject, BirdDying, GameObject, List<GameObject>> BindWindowOfLooseShower;
        public Action<Scores> BindBestScores;
        public Action<BestScoreText> BindBestScoreText;
        
        [SerializeField] private SceneAsset _mainScene;
        [Header("All Main Scene Objects")] 
        [SerializeField] private GameObject _birdPrefab;
        [SerializeField] private GameObject _groundFactoryPrefab, _groundDestroyerPrefab;
        [SerializeField] private GameObject _topPipePrefab, _pipeWithColliderPrefab;
        [SerializeField] private GameObject _pipeWithEffectPrefab;
        [SerializeField] private GameObject _pipesFactoryPrefab, _pipeDestroyerPrefab;
        [SerializeField] private GameObject _bottomBorder, _topBorder;
        [SerializeField] private GameObject _backgroundPrefab;
        [SerializeField] private GameObject _windEffectPrefab;
        [SerializeField] private GameObject _standardUIPrefab;
        [SerializeField] private GameObject _storePrefab;
        [SerializeField] private GameObject _windowOfLoosePrefab, _windowOfLooseShower;
        [SerializeField] private GameObject _currentScorePrefab;
        [SerializeField] private GameObject _eventSystemPrefab;
        [Space(20)] 
        [SerializeField] private SceneLoader _sceneLoader;
        

        private void Awake() => DontDestroyOnLoad(gameObject);

        private void OnEnable() => SceneManager.sceneLoaded += InstantiateAll;
        
        private GameObject InstantiateOnDefaultPosition(GameObject prefab)
        {
            return Instantiate
            (
                prefab, 
                prefab.transform.position,
                prefab.transform.rotation
            );
        }
        
        private GameObject InstantiateStandardUI
        (
        )
        {   
            return InstantiateOnDefaultPosition(_standardUIPrefab);
        }
        
        private GameObject InstantiateCurrentScore()
        {
            return InstantiateOnDefaultPosition(_currentScorePrefab);
        }
        
        private GameObject InstantiateStore(CurrentScore currentScore)
        {
            GameObject store = InstantiateOnDefaultPosition(_storePrefab);
            
            AllScoresText allScoresText =
                store.GetComponentInChildren<AllScoresText>();
            
            BindStore?.Invoke(allScoresText, currentScore);
            return store;
        }
        
        private GameObject InstantiateBottomBorder()
        {
            return InstantiateOnDefaultPosition(_bottomBorder);
        }
        
        private GameObject InstantiateBird(BirdDestroyer birdDestroyer, Store store)
        {
            GameObject bird = InstantiateOnDefaultPosition(_birdPrefab);

            BindBird?.Invoke
            (
                bird,
                birdDestroyer,
                store
            );
            
            return bird;
        }

        private void InstantiateGroundFactory(BirdDying birdDying)
        {
            GameObject ground = InstantiateOnDefaultPosition(_groundFactoryPrefab);
            
            BindGround?.Invoke(ground, birdDying);
        }
        
        private GameObject InstantiatePipesFactory()
        {
            return InstantiateOnDefaultPosition(_pipesFactoryPrefab);
        }

        private GameObject InstantiateWindowOfLoose()
        {
            GameObject windowOfLoose = InstantiateOnDefaultPosition(_windowOfLoosePrefab);
            
            return windowOfLoose;
        }
        

        private GameObject InstantiateWindowOfLooseShower
        (
            BirdDying birdDying,
            GameObject windowOfLoose,
            GameObject standardUI
        )
        {
            List<GameObject> objectsToHide = new List<GameObject>() { standardUI };
            GameObject windowOfLooseShower = InstantiateOnDefaultPosition(_windowOfLooseShower);
            
            BindWindowOfLooseShower?.Invoke
            (
                windowOfLooseShower, 
                birdDying,
                windowOfLoose,
                objectsToHide
            );

            return windowOfLooseShower;
        }
        
        private void InstantiateAll(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != _mainScene.name) return;

            InstantiateOnDefaultPosition(_pipeDestroyerPrefab);
            InstantiateOnDefaultPosition(_groundDestroyerPrefab);
            InstantiateOnDefaultPosition(_bottomBorder);
            InstantiateOnDefaultPosition(_topBorder);
            InstantiateOnDefaultPosition(_backgroundPrefab);
            InstantiateOnDefaultPosition(_windEffectPrefab);
            InstantiateOnDefaultPosition(_eventSystemPrefab);
            
            GameObject standardUI = InstantiateStandardUI();
            Scores scores = standardUI.GetComponentInChildren<Scores>();
            StoreButton storeButton = standardUI.GetComponentInChildren<StoreButton>();

            BindBestScores?.Invoke(scores);
            
            GameObject currentScoreObject = InstantiateCurrentScore();
            CurrentScore currentScore = currentScoreObject.GetComponent<CurrentScore>();
            
            GameObject storeObject = InstantiateStore(currentScore);
            Store store = storeObject.GetComponent<Store>();
            
            BindCurrentScoreAction?.Invoke(currentScoreObject, store, scores);
            
            BirdDestroyer bottomBorder = InstantiateBottomBorder().GetComponent<BirdDestroyer>();
            
            GameObject birdObject = InstantiateBird(bottomBorder, store);
            BirdDying birdDying = birdObject.GetComponent<BirdDying>();

            InstantiateGroundFactory(birdDying);
            GameObject pipesFactoryObject = InstantiatePipesFactory();
            PipesFactory pipesFactory = pipesFactoryObject.GetComponent<PipesFactory>();
            BindPipesFactoryToStandardUI?.Invoke(scores, pipesFactory);
            
            BindPipesFactory?.Invoke
            (
                pipesFactoryObject,
                birdDying,
                scores,
                store
            );
            
            GameObject windowOfLoose = InstantiateWindowOfLoose();

            _sceneLoader.SetMainSceneButton
            (
                windowOfLoose.GetComponentInChildren<MainSceneButton>()
            );
            _sceneLoader.SetUISceneButton
            (
                windowOfLoose.GetComponentInChildren<UISceneButton>()
            );
            BindBestScoreText?.Invoke
            (
                windowOfLoose.GetComponentInChildren<BestScoreText>()
            );
            
            BindWindowOfLoose?.Invoke
            (
                windowOfLoose.GetComponentInChildren<ScoreText>(), 
                scores
            );
            
            WindowOfLooseShower windowOfLooseShower = InstantiateWindowOfLooseShower
            (
                birdDying, 
                windowOfLoose, 
                standardUI
            ).GetComponent<WindowOfLooseShower>();
            
            
            BindStandardUI?.Invoke(scores, windowOfLooseShower, storeButton, storeObject);
        }
        
        private void OnDisable() => SceneManager.sceneLoaded -= InstantiateAll;
    }
}