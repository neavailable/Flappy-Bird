using System;
using UnityEngine;

namespace Scenes
{
    public class MainSceneButton : MonoBehaviour
    {
        public Action ButtonPressed;


        public void GoToMainScene() => ButtonPressed?.Invoke();
    }
}