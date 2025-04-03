using MainScene.UserInput;
using MainScene.Object.SkinsManager;
using UnityEditor.Animations;
using UnityEngine;

namespace MainScene.Bird
{
    [RequireComponent(typeof(Animator), typeof(BirdInput), typeof(BirdSkinsManager))]
    public class BirdAnimations : MonoBehaviour
    {
        private const string _flyAnimationName = "Fly";
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
            _animator.Play(_flyAnimationName);
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