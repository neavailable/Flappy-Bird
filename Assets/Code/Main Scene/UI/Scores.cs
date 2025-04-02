using System;
using System.Collections.Generic;
using MainScene.Pipe;
using TMPro;
using MainScene.ThroughPipe;
using MainScene.WindowOfLoose;
using UnityEngine;

namespace MainScene.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class Scores : MonoBehaviour
    {
        public Action<int> SendCurrentScoreAction;
        public Action ScoreIsRound, AddPoint;

        [SerializeField] private StoreButton _storeButton;
        private PipesFactory _pipesFactory;
        private WindowOfLooseShower _windowOfLooseShower;
        private List<ThroughPipeCollider> _throughPipeColliders;
        private TMP_Text _scoreText;
        private int _scoreNumberWithEffect, _currentScore;


        public void Constructor(PipesFactory pipesFactory)
        {
            _pipesFactory = pipesFactory;

            _pipesFactory.BottomPipeCreated += AddThroughPipeCollider;
            
            _scoreNumberWithEffect = _pipesFactory.PipeNumberWithEffect;
            
            _throughPipeColliders = new List<ThroughPipeCollider>();
            
            _storeButton.StoreOpened += SendCurrentScore;
            _scoreText = GetComponent<TMP_Text>();
            _currentScore = 0;
            _scoreText.text = _currentScore.ToString();
        }
        
        public void Constructor(WindowOfLooseShower windowOfLooseShower)
        {
            _windowOfLooseShower = windowOfLooseShower;
            
            _windowOfLooseShower.SendScore += SendCurrentScore;
        }
        
        private void AddThroughPipeCollider(ThroughPipeCollider throughPipeCollider)
        {
            throughPipeCollider.ThroughPipeColliderAction += ChangeScore;

            _throughPipeColliders.Add(throughPipeCollider);
        }

        private void ChangeScore()
        {
            _scoreText.text = (++_currentScore).ToString();
            AddPoint?.Invoke();
            if (_currentScore % _scoreNumberWithEffect == 0) ScoreIsRound?.Invoke();
        }

        private void SendCurrentScore()
        {
            SendCurrentScoreAction?.Invoke(_currentScore);
        }

        private void OnDestroy()
        {
            _storeButton        .StoreOpened       -= SendCurrentScore;
            _pipesFactory       .BottomPipeCreated -= AddThroughPipeCollider;
            _windowOfLooseShower.SendScore         -= SendCurrentScore;

            foreach (ThroughPipeCollider throughPipeCollider in _throughPipeColliders)
            {
                throughPipeCollider.ThroughPipeColliderAction -= ChangeScore;
            }
            _throughPipeColliders.Clear();
        }
    }
}