using UnityEngine;


namespace MainScene.Pipe
{
    [RequireComponent(typeof(PipesFactory))]
    public class PipesLocator : MonoBehaviour
    {
        [Range(8f, 10f), SerializeField] private float _minGapBetweenPipes;
        [Range(10f, 12f), SerializeField] private float _maxGapBetweenPipes;
        [SerializeField] private Transform _lowestStartPoint, _highestStartPoint;
        private PipesFactory _pipesFactory;
        private System.Random _random;


        private void OnEnable()
        {
            _pipesFactory = GetComponent<PipesFactory>();
            _random = new System.Random();

            _pipesFactory.LocateBottomPipe += LocateBottomPipe;
            _pipesFactory.LocateTopPipe    += LocateTopPipe;
        }

        private float GetRandomY(float min, float max)
        {
            return (float) (_random.NextDouble() * (max - min) + min);
        }

        private Vector2 LocateBottomPipe()
        {
            float min = _lowestStartPoint. position.y;
            float max = _highestStartPoint.position.y;

            float y = GetRandomY(min, max);

            return new Vector2(_lowestStartPoint.position.x, y);
        }

        private Vector2 LocateTopPipe(Vector2 pipeLocation)
        {
            float GapY = GetRandomY(_minGapBetweenPipes, _maxGapBetweenPipes);
            
            return new Vector2
            (
                pipeLocation.x,
                pipeLocation.y + GapY
            );
        }

        private void OnDisable()
        {
            _pipesFactory.LocateBottomPipe -= LocateBottomPipe;
            _pipesFactory.LocateTopPipe    -= LocateTopPipe;
        }
    }
}
