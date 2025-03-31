using MainScene.UI;
using UnityEngine;

namespace MainScene.ThroughPipe
{
    public class ThroughPipeEffectFactory : MonoBehaviour
    {
        [SerializeField] private Transform _throughPipeEffectTransform;
        private GameObject _throughPipeEffectPrefab;
        private Scores _scores;
        
        
        public void Constructor(GameObject throughPipeEffectPrefab, Scores scores)
        {
            _throughPipeEffectPrefab = throughPipeEffectPrefab;
            _scores = scores;
            
            _scores.ScoreIsRound += InstantiateThroughPipeEffect;
        }
        
        private void InstantiateThroughPipeEffect()
        { 
            Instantiate
            (
                _throughPipeEffectPrefab,
                _throughPipeEffectTransform.position,
                _throughPipeEffectPrefab.transform.rotation 
            ).transform.SetParent(transform.parent);
        }
        
        private void OnDestroy()
        {
            if (_scores) _scores.ScoreIsRound -= InstantiateThroughPipeEffect;
        }
    }
}