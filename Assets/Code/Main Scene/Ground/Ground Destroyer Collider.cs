using UnityEngine;

namespace MainScene.Ground
{
    public class GroundDestroyerCollider : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.TryGetComponent<DestroyGround>
            (
                out DestroyGround destroyGround
            ))
            {
                destroyGround.SelfDestroy();
            }
        }
    }
}