using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Player
{
    public class PlayerAnimation : MonoBehaviour
    {
        private Animator _animator;
        private PlayerMovement _playerMovement;
        
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnCharacterDead, TriggerDeathAnimation);
            
            Initialize();
        }
        
        private void Initialize()
        {
            _animator = GetComponent<Animator>();
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnCharacterDead, TriggerDeathAnimation);
        }
        
        private void Update()
        {
            SetBlendValue();
        }
        
        private void SetBlendValue()
        {
            var speed = _playerMovement.GetPlayerVelocity();
            
            _animator.SetFloat(ProjectConstants.AnimationBlendValue, speed);
        }

        public void TriggerAnimation(string animationName, float animationSpeed = 1f)
        {
            _animator.SetTrigger(animationName);
            _animator.SetFloat(ProjectConstants.AnimationAnimationSpeed, animationSpeed);
        }
        
        public void ResetTriggerAnimation(string animationName)
        {
            _animator.ResetTrigger(animationName);
        }
        
        private void TriggerDeathAnimation(EventParameters eventParameters)
        {
            if (eventParameters.CharacterGameObject != gameObject) return;
            
            TriggerAnimation(ProjectConstants.AnimationDeath);
        }
    }
}
