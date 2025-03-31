namespace MainScene.Item.Bird
{
    public class RedBirdSkin : Skin, IBirdSkin
    {
        private void Awake()
        { 
            Item = new Item(0);
        }
    }
}