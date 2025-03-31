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
        
        private void OnEnable()
        {
            _store.BuyItem += ChangeSkin;
        }
        
        private void OnDisable()
        {
            _store.BuyItem -= ChangeSkin;
        }
    }
}