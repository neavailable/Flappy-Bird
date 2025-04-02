using System;
using UnityEngine;
using MainScene.UI;

namespace MainScene.Counters
{
    public class BestScoreCounter : MonoBehaviour
    {
        public Action<int> SendBestScore;
        
        private Scores _scores;
        private int _bestScore;


        public void SetScores(Scores scores)
        {
            _scores = scores;
            _scores.SendCurrentScoreAction += ChangeBestScore;    
        }
        
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _bestScore = 0;
        }
        
        private void ChangeBestScore(int score)
        {
            if (score > _bestScore) _bestScore = score;
            
            SendBestScore?.Invoke(_bestScore);
        }

        private void OnDestroy()
        {
            if (_scores) _scores.SendCurrentScoreAction -= ChangeBestScore;
        }
    }
}