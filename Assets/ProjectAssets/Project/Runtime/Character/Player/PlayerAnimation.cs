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
            _animator.SetFloat(ProjectConstants.AnimationBlendValue,
                _playerMovement.GetInputMagnitude(), 0.05f, Time.deltaTime);
        }

        public void TriggerAnimation(string animationName)
        {
            _animator.SetTrigger(animationName);
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
