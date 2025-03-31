using TMPro;
using UnityEngine;
using MainScene.UI;
using System;

namespace MainScene.WindowOfLoose
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreText : MonoBehaviour
    {   
        [SerializeField] private Scores _scores;
        private const string _defaultText = "Score\n";
        private TMP_Text _text;
        

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            
            _scores.SendCurrentScoreAction += SetScore;
        }

        private void SetScore(int score)
        {
            _text.text = _defaultText + score;
        }
        
        private void OnDestroy()
        {
            _scores.SendCurrentScoreAction -= SetScore;
        }
    }
}