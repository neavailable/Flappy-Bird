using UnityEngine;
using System;
using System.Collections.Generic;
using MainScene.Bird;
using MainScene.Object;
using MainScene.Object.SkinsManager;
using MainScene.ThroughPipe;
using MainScene.UI;

namespace MainScene.Pipe
{
    [RequireComponent(typeof(PipesCreatorMark), typeof(AllPipesSkinsManager))]
    public class PipesFactory : MonoBehaviour
    {
        public Func<Vector2> LocateBottomPipe;
        public Func<Vector2, Vector2> LocateTopPipe;
        public Action<ThroughPipeCollider> BottomPipeCreated;
        public int PipeNumberWithEffect => _pipeNumberWithEffect;
        
        [Header("Bottom Pipe Prefabs")]
        [SerializeField] private GameObject _pipeWithEffect, _pipeWithCollider;
        [Header("Top Pipe Prefab")]
        [SerializeField] private GameObject _topPipe, _throughPipeEffectPrefab;
        [SerializeField] private BirdDying _birdDying;
        [SerializeField] private Store.Store _store;
        [SerializeField] private Scores _scores;
        private AllPipesSkinsManager _allPipesSkinsManager;
        [Range(5, 15), SerializeField] private int _pipeNumberWithEffect;
        private List<ObjectCollider> _pipesColliders;
        private int _currentNumberOfPipes;

        
        private void Start()
        {
            _allPipesSkinsManager = GetComponent<AllPipesSkinsManager>();
            _pipesColliders = new List<ObjectCollider>();
            _currentNumberOfPipes = 1;
            InstantiatePipes();
        }

        private void BindTopPipe(GameObject pipe)
        {
            pipe.GetComponent<ObjectMovement>().Constructor(_birdDying);
            PipeSkinsManager pipeSkinsManager = pipe.GetComponent<PipeSkinsManager>();
            DestroyPipe destroyPipe = pipe.GetComponentInChildren<DestroyPipe>();
            _allPipesSkinsManager.AddPipeSkinsManager(pipeSkinsManager, destroyPipe);
            
            ObjectCollider objectCollider = pipe.GetComponentInChildren<ObjectCollider>();
            objectCollider.CreatePipeAction += InstantiatePipes;
            _pipesColliders.Add(objectCollider);
        }

        private void BindPipeWithCollider(GameObject pipe)
        {
            BindTopPipe(pipe);
            ThroughPipeCollider throughPipeCollider = pipe.
                GetComponentInChildren<ThroughPipeCollider>();
            if (throughPipeCollider == null)
            {
                Debug.Log("There is no children with ThroughPipeCollider component");
                return;
            }
            BottomPipeCreated?.Invoke(throughPipeCollider);
        }
        
        private void BindPipeWithEffect(GameObject pipe)
        {
            BindPipeWithCollider(pipe);

            ThroughPipeEffectFactory throughPipeEffectFactory = pipe.
                GetComponentInChildren<ThroughPipeEffectFactory>();
            if (!throughPipeEffectFactory)
            {
                Debug.Log("There is no children with ThroughPipeEffectFactory component");
                return;
            }
            throughPipeEffectFactory.Constructor(_throughPipeEffectPrefab, _scores);
        }

        private void DecideTypeOfPipe(Vector2 location)
        {
            if (_currentNumberOfPipes % _pipeNumberWithEffect == 0)
            {
                BindPipeWithEffect
                (
                    Instantiate(_pipeWithEffect, location,  Quaternion.identity)
                );
            }
            else
            {
                BindPipeWithCollider
                (
                    Instantiate(_pipeWithCollider, location, Quaternion.identity)
                );
            }
        }
        
        private void InstantiatePipes()
        {
            Vector2 pipeLocation = LocateBottomPipe.Invoke(); 
            DecideTypeOfPipe(pipeLocation);

            BindTopPipe
            (
                Instantiate
                (
                    _topPipe,
                    LocateTopPipe.Invoke(pipeLocation),
                    Quaternion.Euler(x: 0, y: 0, z: 180)
                )
            );
            ++_currentNumberOfPipes;
        }

        private void OnDestroy()
        {
            foreach (var pipeCollider in _pipesColliders)
            {
                pipeCollider.CreatePipeAction -= InstantiatePipes;
            }
        }
    }
}