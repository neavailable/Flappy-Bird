using UnityEngine;
using MainScene.UserInput;
using MainScene.Object.SkinsManager;
using UnityEditor.Animations;

namespace MainScene.Bird
{
    [RequireComponent(typeof(Animator), typeof(BirdInput), typeof(BirdSkinsManager))]
    public class BirdAnimations : MonoBehaviour
    {
        private Animator _animator;
        private BirdSkinsManager _birdSkinsManager;
        private BirdInput _birdInput;

        
        private void OnEnable()
        {
            _birdInput = GetComponent<BirdInput>();
            _birdSkinsManager = GetComponent<BirdSkinsManager>();

            _birdSkinsManager.ChangeAnimatorController += ChangeAnimatorController;
            _birdInput.SpaceClicked += PlayFlyAnimation;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void PlayFlyAnimation(bool _)
        {
            _animator.Play("Fly");
        }

        private void ChangeAnimatorController(AnimatorController _animatorController)
        {
            _animator.runtimeAnimatorController = _animatorController;
        }
        
        private void OnDisable()
        {
            _birdSkinsManager.ChangeAnimatorController -= ChangeAnimatorController;
            _birdInput.SpaceClicked -= PlayFlyAnimation;
        }
    }
}