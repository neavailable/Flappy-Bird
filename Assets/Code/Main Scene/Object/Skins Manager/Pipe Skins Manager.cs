using MainScene.Item.Pipe;

namespace MainScene.Object.SkinsManager
{
    public class PipeSkinsManager : SkinsManager
    {
        public bool CanChangeSkin(Item.Skin skin)
        {
            return skin is IPipeSkin;
        }
    }
}