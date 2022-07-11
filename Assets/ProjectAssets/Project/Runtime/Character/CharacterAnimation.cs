using ProjectAssets.Project.Runtime.Core;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Project.Runtime.Character
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CharacterMovement))]
    
    public class CharacterAnimation : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        private CharacterMovement _characterMovement;

        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnCharacterDead, TriggerDeathAnimation);
            
            Initialize();
        }

        private void Initialize()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _characterMovement = GetComponent<CharacterMovement>();
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
            var velocityVector = _characterMovement.GetAgentVelocity();
            var localVelocity = transform.InverseTransformDirection(velocityVector);
            var speed = localVelocity.z;
            
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