using UnityEngine;


namespace MainScene.Ground
{
    [RequireComponent(typeof(GroundFactory))]
    public class GroundLocator : MonoBehaviour
    {
        [SerializeField] private Transform _groundStartPoint;
        private GroundFactory _groundFactory;


        private void OnEnable()
        {
            _groundFactory = GetComponent<GroundFactory>();
            
            _groundFactory.LocateGround += LocateGround;
        }

        private Vector2 LocateGround()
        {
            return _groundStartPoint.position;
        }

        private void OnDisable()
        {
            _groundFactory.LocateGround -= LocateGround;
        }
    }
}
