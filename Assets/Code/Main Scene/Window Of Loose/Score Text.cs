using TMPro;
using MainScene.UI;
using UnityEngine;

namespace MainScene.WindowOfLoose
{
    [RequireComponent(typeof(TMP_Text))]
    public class ScoreText : MonoBehaviour
    {   
        private Scores _scores;
        private const string _defaultText = "Score\n";
        private TMP_Text _text;


        public void Constructor(Scores scores)
        {
            _text = GetComponent<TMP_Text>();

            _scores = scores;
            
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