using MainScene.Item;
using System.Collections.Generic;
using MainScene.Pipe;
using UnityEngine;

namespace MainScene.Object.SkinsManager
{
    public class AllPipesSkinsManager : MonoBehaviour
    {
        [SerializeField] private Store.Store _store;
        private List<PipeSkinsManager> _pipeSkinsManagers;
        private List<DestroyPipe> _destroyPipes;
        private Skin _currentSkin;


        public void Constructor(Store.Store store)
        {
            _store = store;
            _store.BuyItem += ChangeCurrentSkin;
            
            _pipeSkinsManagers = new List<PipeSkinsManager>();
            _destroyPipes      = new List<DestroyPipe>();
        }
        
        public void AddPipeSkinsManager
        (
            PipeSkinsManager pipeSkinsManager,
            DestroyPipe destroyPipe
        )
        {
            _pipeSkinsManagers.Add(pipeSkinsManager);
            _destroyPipes.Add(destroyPipe);
            
            destroyPipe.SelfDestroyAction += RemovePipe;
            if (_currentSkin) pipeSkinsManager.ChangeSkin(_currentSkin);
        }
        
        private void RemovePipe(GameObject pipe)
        {
            PipeSkinsManager pipeSkinsManagers = pipe.GetComponent<PipeSkinsManager>();
            
            if (pipeSkinsManagers == null) return;
            
            RemovePipeSkinsManager(pipeSkinsManagers);
        }
        
        private void RemovePipeSkinsManager(PipeSkinsManager pipeSkinsManager)
        {
            _pipeSkinsManagers.Remove(pipeSkinsManager);
        }
        
        private bool ChangeCurrentSkin(Skin skin)
        {
            if (_pipeSkinsManagers.Count > 0 && !_pipeSkinsManagers[0].CanChangeSkin(skin)) return false;
            
            _currentSkin = skin;

            foreach (PipeSkinsManager pipeSkinsManager in _pipeSkinsManagers)
            {
                pipeSkinsManager.ChangeSkin(_currentSkin);
            }
            
            return true;
        }
        
        private void OnDestroy()
        {
            _store.BuyItem -= ChangeCurrentSkin;

            foreach (var destroyPipe in _destroyPipes)
            {
                if (destroyPipe != null)
                {
                    destroyPipe.SelfDestroyAction -= RemovePipe;
                }
            }
        }
    }
}