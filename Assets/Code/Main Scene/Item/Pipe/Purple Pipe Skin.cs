namespace MainScene.Item.Pipe
{
    public class PurplePipeSkin : Skin, IPipeSkin
    {
        private void Awake()
        {
            Item = new Item(10);
        }
    }
}