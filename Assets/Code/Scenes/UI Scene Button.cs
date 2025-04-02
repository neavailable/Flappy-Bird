using UnityEngine;
using System;

namespace Scenes
{
    public class UISceneButton : MonoBehaviour
    {
        public Action ButtonPressed;
        
        
        public void GoToUIScene() => ButtonPressed?.Invoke();
    }
}