using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace MainScene.Counters
{
    public class BestScoreCounter : MonoBehaviour
    {
        public Action<int> SendBestScore;

        private static bool _wasCreated = false;
        
        private UI.Scores _scores;
        private int _bestScore;

        
        private void Awake()
        {
            if (_wasCreated)
            {
                Destroy(gameObject);
                return;
            }
            
            DontDestroyOnLoad(gameObject);
            _wasCreated = true;
            _bestScore = 0;
        }

        public void Update()
        {
            if (SceneManager.GetActiveScene().name == "Main Scene" && !_scores)
            {
                _scores = FindObjectOfType<UI.Scores>();
                _scores.SendCurrentScoreAction += ChangeBestScore;
            }
        }
        
        private void ChangeBestScore(int score)
        {
            if (score > _bestScore)
            {
                _bestScore = score;
            } 
            
            SendBestScore?.Invoke(_bestScore);
        }

        private void OnDestroy()
        {
            if (_scores) _scores.SendCurrentScoreAction -= ChangeBestScore;
        }
    }
}