using System;
using UnityEngine;

namespace MainScene.Store.StoreItem
{
    [RequireComponent(typeof(Item.Skin))]
    public abstract class StoreItem : MonoBehaviour
    {
        public Action<StoreItem, Item.Skin> BuyAction;

        public Item.Skin Skin { get; protected set; }


        private void Awake()
        {
            Skin = GetComponent<Item.Skin>();
        }
        
        public void Buy()
        {
            BuyAction?.Invoke(this, Skin);
        }
    }
}