using TMPro;
using UnityEngine;

namespace MainScene.Store
{
    [RequireComponent(typeof(TMP_Text), typeof(RectTransform))]
    public class BuyButtonText : MonoBehaviour
    {
        [SerializeField] private Vector3 _textAngle;
        [SerializeField] private Store _store;
        private RectTransform _rectTransform;
        private TMP_Text _text;
        private Item.Skin _skin;
        private const string _soldText = "Sold";


        private void Awake()
        {
            _store.BuyItem += ChangeText;

            _rectTransform = GetComponent<RectTransform>();
            _text = GetComponent<TMP_Text>();
            StoreItem.StoreItem storeItem = GetComponentInParent<StoreItem.StoreItem>();
            
            _skin = storeItem.Skin;
            _text.text = _skin.Item.Price.ToString();
        }

        private bool ChangeText(Item.Skin skin)
        {
            if (_skin != skin) return false;
            
            _text.text = _soldText;

            _rectTransform.rotation = Quaternion.Euler(_textAngle);
            return true;
        }
        
        private void OnDestroy()
        {
            _store.BuyItem -= ChangeText;
        }
    }
}