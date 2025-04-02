using System;
using MainScene.Pipe;
using UnityEngine;

namespace MainScene.Object
{
    public class ObjectCollider : MonoBehaviour
    {
        public Action CreatePipeAction, SelfDestroyAction;


        private void KillBird(Collider2D collision)
        {
            if (collision.TryGetComponent<Bird.BirdDying>
            (
                out Bird.BirdDying birdDying
            ))
            {
                birdDying.StartDying();
            }
        }

        private void CreatePipe(Collider2D collision)
        {
            if (collision.TryGetComponent<PipesCreatorMark>
                (
                    out PipesCreatorMark _
                ))
            {
                CreatePipeAction?.Invoke();
            }
        }
        
        private void SelfDestroy(Collider2D collision)
        {
            if (collision.TryGetComponent<PipesDestroyerMark>
            (
                out PipesDestroyerMark _
            ))
            {
                SelfDestroyAction?.Invoke();
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {  
            KillBird(collision);
            CreatePipe(collision);
            SelfDestroy(collision);
        }
    }
}
