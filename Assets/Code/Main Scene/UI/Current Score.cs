using System;
using UnityEngine;

namespace MainScene.UI
{
    public class CurrentScore : MonoBehaviour
    {
        public Action ChangeAllScores;
        public int CurentScore => _curentScore;
        
        // -----
        [SerializeField] private Store.Store _store;
        [SerializeField] private Scores _scores;
        // -----
        private int _curentScore;


        public void Constructor(Store.Store store, Scores scores)
        {
            _store = store;
            _scores = scores;
            
            _scores.AddPoint       += AddPoint;
            _store .SubtractPoints += SubtractAllScores;
            _curentScore = 0;

        }
        
        private void AddPoint()
        {
            ++_curentScore;
            ChangeAllScores?.Invoke();
        }
        
        private bool SubtractAllScores(int scores)
        {
            if (_curentScore - scores < 0) return false;
            
            _curentScore -= scores;
            ChangeAllScores?.Invoke();
            return true;
        }
        
        private void OnDestroy()
        {
            if (_scores) _scores.AddPoint       -= AddPoint;  
            if (_store)  _store .SubtractPoints -= SubtractAllScores;
        }
    }
}