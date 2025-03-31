namespace MainScene.Item.Bird
{
    public class BlueBirdSkin : Skin, IBirdSkin
    {
        private void Awake()
        {
            Item = new Item(5);
        }
    }
}