using System;
using System.Collections.Generic;
using MainScene.Pipe;
using TMPro;
using UnityEngine;
using MainScene.ThroughPipe;
using MainScene.WindowOfLoose;

namespace MainScene.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class Scores : MonoBehaviour
    {
        public Action<int> SendCurrentScoreAction;
        public Action ScoreIsRound, AddPoint;

        [SerializeField] private PipesFactory _pipesFactory;
        [SerializeField] private StoreButton _storeButton;
        [SerializeField] private WindowOfLooseShower _windowOfLooseShower;
        private List<ThroughPipeCollider> _throughPipeColliders;
        private TMP_Text _scoreText;
        private int _scoreNumberWithEffect, _currentScore;
        

        private void OnEnable()
        {
            _throughPipeColliders = new List<ThroughPipeCollider>();

            _pipesFactory       .BottomPipeCreated += AddThroughPipeCollider;
            _storeButton        .StoreOpened       += SendCurrentScore;
            _windowOfLooseShower.SendScore         += SendCurrentScore;
        }
        
        private void Start()
        {
            _scoreText = GetComponent<TMP_Text>();

            _scoreNumberWithEffect = _pipesFactory.PipeNumberWithEffect;
            _currentScore = 0;
            _scoreText.text = _currentScore.ToString();
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
        
        private void OnDisable()
        {
            _pipesFactory       .BottomPipeCreated -= AddThroughPipeCollider;
            _storeButton        .StoreOpened       -= SendCurrentScore;
            _windowOfLooseShower.SendScore         -= SendCurrentScore;
        }

        private void OnDestroy()
        {
            foreach (ThroughPipeCollider throughPipeCollider in _throughPipeColliders)
            {
                throughPipeCollider.ThroughPipeColliderAction -= ChangeScore;
            }
            _throughPipeColliders.Clear();
        }
    }
}