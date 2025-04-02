using MainScene.Item;
using UnityEngine;

namespace MainScene.Object.SkinsManager
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class SkinsManager : MonoBehaviour
    {
        private SpriteRenderer _spriteRender;
        
        
        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }
        
        public virtual bool ChangeSkin(Skin skin)
        {
            if  (_spriteRender.sprite == skin.Sprite) return false;
            
            _spriteRender.sprite = skin.Sprite;
            
            return true;
        }
    }
}