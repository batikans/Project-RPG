using ProjectAssets.Project.Runtime.Core;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Project.Runtime.Character.AI
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AIMovement))]
    
    public class AIAnimation : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        private AIMovement _aiMovement;

        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnCharacterDead, TriggerDeathAnimation);
            
            Initialize();
        }

        private void Initialize()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
            _aiMovement = GetComponent<AIMovement>();
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
            var velocityVector = _aiMovement.GetAgentVelocity();
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