using System.Collections.Generic;
using UnityEngine;
using MainScene.Bird;
using MainScene.Counters;
using MainScene.Ground;
using MainScene.Object.SkinsManager;
using MainScene.Pipe;
using MainScene.Store;
using MainScene.UI;
using MainScene.WindowOfLoose;

namespace BootstrapScene
{
    [RequireComponent(typeof(MainSceneFactory))]
    public class MainSceneBinder : MonoBehaviour
    {
        [SerializeField] private BestScoreCounter _bestScoreCounter;
        private MainSceneFactory _mainSceneFactory;


        private void OnEnable()
        {
            _mainSceneFactory = GetComponent<MainSceneFactory>();
            
            _mainSceneFactory.BindBird                     += BindBird;
            _mainSceneFactory.BindGround                   += BindGround;
            _mainSceneFactory.BindPipesFactory             += BindPipesFactory;
            _mainSceneFactory.BindStore                    += BindStore;
            _mainSceneFactory.BindCurrentScoreAction       += BindCurrentScore;
            _mainSceneFactory.BindPipesFactoryToStandardUI += BindPipesFactoryToStandardUI;
            _mainSceneFactory.BindStandardUI               += BindStandardUI;
            _mainSceneFactory.BindWindowOfLoose            += BindWindowOfLoose;
            _mainSceneFactory.BindWindowOfLooseShower      += BindWindowOfLooseShower;
            _mainSceneFactory.BindBestScores               += BindBestScores; 
            _mainSceneFactory.BindBestScoreText            += BindBestScoreText;
        }

        private void BindBird(GameObject bird, BirdDestroyer birdDestroyer, Store store)
        {
            bird.GetComponent<BirdDying>().Constructor(birdDestroyer);
            bird.GetComponent<BirdSkinsManager>().Constructor(store);
        }

        private void BindGround(GameObject ground, BirdDying birdDying)
        {
            ground.GetComponent<GroundFactory>().Constructor(birdDying);
        }

        private void BindPipesFactory
        (
            GameObject pipesFactory,
            BirdDying birdDying,
            Scores scores,
            Store store
        )
        {
            pipesFactory.GetComponent<AllPipesSkinsManager>().Constructor(store);
            pipesFactory.GetComponent<PipesFactory>().Constructor(birdDying, scores);
        }

        private void BindStore(AllScoresText allScoresText, CurrentScore currentScore)
        {
            allScoresText.Constructor(currentScore);
        }

        private void BindCurrentScore(GameObject currentScore, Store store, Scores scores)
        {
            currentScore.GetComponent<CurrentScore>().Constructor(store, scores);
        }

        private void BindPipesFactoryToStandardUI(Scores scores, PipesFactory pipesFactory)
        {
            scores.Constructor(pipesFactory);
        }

        private void BindStandardUI
        (
            Scores scores,
            WindowOfLooseShower windowOfLooseShower,
            StoreButton storeButton,
            GameObject store
        )
        {
            scores.Constructor(windowOfLooseShower);
            storeButton.Constructor(store);
        }

        private void BindWindowOfLoose(ScoreText scoreText, Scores scores)
        {
            scoreText.Constructor(scores);
        }
        
        private void BindWindowOfLooseShower
        (
            GameObject windowOfLooseShower,
            BirdDying birdDying,
            GameObject windowOfLoose,
            List<GameObject> objectsToHide
        )
        {
            windowOfLooseShower.GetComponent<WindowOfLooseShower>().
                Constructor(birdDying, windowOfLoose, objectsToHide);
        }

        private void BindBestScores(Scores scores) => _bestScoreCounter.SetScores(scores);

        private void BindBestScoreText(BestScoreText bestScoreText)
        {
            bestScoreText.Constructor(_bestScoreCounter);
        }
        
        private void OnDisable()
        {
            _mainSceneFactory.BindBird                     -= BindBird;
            _mainSceneFactory.BindGround                   -= BindGround;
            _mainSceneFactory.BindPipesFactory             -= BindPipesFactory;
            _mainSceneFactory.BindStore                    -= BindStore;
            _mainSceneFactory.BindCurrentScoreAction       -= BindCurrentScore;
            _mainSceneFactory.BindPipesFactoryToStandardUI -= BindPipesFactoryToStandardUI;
            _mainSceneFactory.BindStandardUI               -= BindStandardUI;
            _mainSceneFactory.BindWindowOfLoose            -= BindWindowOfLoose;
            _mainSceneFactory.BindWindowOfLooseShower      -= BindWindowOfLooseShower;
            _mainSceneFactory.BindBestScores               -= BindBestScores;
            _mainSceneFactory.BindBestScoreText            -= BindBestScoreText;
        }
    }
}