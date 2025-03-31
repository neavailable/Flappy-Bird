using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace MainScene.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class AllScoresText : MonoBehaviour
    {
        [SerializeField] private CurrentScore _currentScore;
        private TMP_Text _allScoresText;
        

        private void Awake()
        {
            _allScoresText = GetComponent<TMP_Text>();

            _allScoresText.text = _currentScore.CurentScore.ToString();

            _currentScore.ChangeAllScores += ChangeAllScoresText;
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