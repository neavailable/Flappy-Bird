namespace MainScene.Item.Pipe
{
    public class GreenPipeSkin : Skin, IPipeSkin
    {
        private void Awake()
        {
            Item = new Item(0);
        }
    }
}