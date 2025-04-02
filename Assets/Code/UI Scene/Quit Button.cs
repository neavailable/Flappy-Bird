using UnityEngine;

namespace UIScene.Quit
{
    public class QuitButton : MonoBehaviour
    {
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}