using System;
using System.Collections.Generic;
using UnityEngine;

namespace MainScene.Store
{
    public class Store : MonoBehaviour
    {
        public Func<int, bool> SubtractPoints;
        public Func<Item.Skin, bool> BuyItem;
        
        [SerializeField] private List<StoreItem.StoreItem> _items;


        private void Awake()
        {   
            foreach (var item in _items) item.BuyAction += Buy;
        }

        private void Buy(StoreItem.StoreItem storeItem, Item.Skin skin)
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                if (_items[i].GetType() == storeItem.GetType())
                {
                    if (SubtractPoints.Invoke(skin.Item.Price))
                    {
                        BuyItem?.Invoke(skin);
                        _items.RemoveAt(i);
                        break;
                    }
                }
            }
        }
        
        private void OnDestroy()
        {
            foreach (var item in _items) item.BuyAction -= Buy;
            _items.Clear();
        }
    }
}