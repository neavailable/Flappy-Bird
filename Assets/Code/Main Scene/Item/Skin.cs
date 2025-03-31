using UnityEditor.Animations;
using UnityEngine;

namespace MainScene.Item
{
    public abstract class Skin : MonoBehaviour
    {
        public Item Item { get; protected set; }
        public Sprite Sprite => _sprite;
        public AnimatorController AnimatorController => _animatorController;
        
        [SerializeField] private Sprite _sprite;
        [SerializeField] private AnimatorController _animatorController;
        private int _price;
    }
}