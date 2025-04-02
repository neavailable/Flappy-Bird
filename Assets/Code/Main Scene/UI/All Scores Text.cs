using TMPro;
using UnityEngine;

namespace MainScene.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class AllScoresText : MonoBehaviour
    {
        private CurrentScore _currentScore;
        private TMP_Text _allScoresText;

        
        public void Constructor(CurrentScore currentScore)
        {
            _currentScore = currentScore;
            
            _currentScore.ChangeAllScores += ChangeAllScoresText;
            
            _allScoresText = GetComponent<TMP_Text>();
            _allScoresText.text = _currentScore.CurentScore.ToString();
        }
        
        private void ChangeAllScoresText()
        {
            _allScoresText.text = _currentScore.CurentScore.ToString();
        }

        private void OnDestroy()
        {
            _currentScore.ChangeAllScores -= ChangeAllScoresText;
        }
    }
}