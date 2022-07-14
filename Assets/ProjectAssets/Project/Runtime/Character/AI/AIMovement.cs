using ProjectAssets.Project.Runtime.Core;
using ProjectAssets.Project.Runtime.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Project.Runtime.Character.AI
{
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ActionScheduler))]
    public class AIMovement : MonoBehaviour, IAction, ISaveable
    {
        private NavMeshAgent _agent;
        private ActionScheduler _actionScheduler;
        private CharacterStats _characterStats;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _agent = GetComponent<NavMeshAgent>();
            _characterStats = GetComponent<CharacterStats>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        public void MoveToDestination(Vector3 destination, float speedFraction,float stoppingDistance = 0f)
        {
            _actionScheduler.StartAction(this);
            
            SetAgentStoppingDistance(stoppingDistance);
            _agent.speed = _characterStats.currentMaxMovementSpeed * Mathf.Clamp01(speedFraction);
            _agent.SetDestination(destination);
        }

        private void SetAgentStoppingDistance(float distance)
        {
            _agent.stoppingDistance = distance;
        }

        public Vector3 GetAgentVelocity()
        {
            return _agent.velocity;
        }

        public void DisableAgent()
        {
            _agent.enabled = false;
        }

        public void EnableAgent()
        {
            _agent.enabled = true;
        }

        public void CancelAction()
        {
            //TODO: Cancel Movement
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            var savedPosition = (SerializableVector3) state;
            _agent.Warp(savedPosition.ToVector());
            //transform.position = savedPosition.ToVector();
        }
    }
}

