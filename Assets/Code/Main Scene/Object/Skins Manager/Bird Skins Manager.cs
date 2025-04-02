using System;
using MainScene.Item;
using MainScene.Item.Bird;
using UnityEditor.Animations;
using UnityEngine;

namespace MainScene.Object.SkinsManager
{
    public class BirdSkinsManager : SkinsManager
    {
        public Action<AnimatorController> ChangeAnimatorController;
        
        [SerializeField] private Store.Store _store;


        public void Constructor(Store.Store store)
        {
            _store = store;
            _store.BuyItem += ChangeSkin;
        }
        
        public override bool ChangeSkin(Skin skin)
        {
            if (!CanChangeSkin(skin)) return false;
            
            base.ChangeSkin(skin);
            
            ChangeAnimatorController?.Invoke(skin.AnimatorController);
            return true;
        }


        private bool CanChangeSkin(Skin skin)
        {
            return skin is IBirdSkin;
        }
        
        private void OnDestroy()
        {
            _store.BuyItem -= ChangeSkin;
        }
    }
}