using TMPro;
using UnityEngine;

namespace MainScene.WindowOfLoose
{
    [RequireComponent(typeof(TMP_Text))]
    public class BestScoreText : MonoBehaviour
    {
        private Counters.BestScoreCounter _bestScoreCounter;
        private const string _defaultText = "Best Score\n";
        private TMP_Text _text; 

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();

            const int startScore = 0;
            _text.text = _defaultText + startScore;
            _bestScoreCounter = FindObjectOfType<Counters.BestScoreCounter>();
            _bestScoreCounter.SendBestScore += SetBestScore;
        }
        
        private void SetBestScore(int score)
        {
            _text.text = _defaultText + score;
        }
        
        private void OnDestroy()
        {
            _bestScoreCounter.SendBestScore -= SetBestScore;
        }
    }
}