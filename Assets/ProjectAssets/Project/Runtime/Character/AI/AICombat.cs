using ProjectAssets.Project.Runtime.Core;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Project.Runtime.Character.AI
{
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterCombatTarget))]
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(AIAnimation))]
    public class AICombat : MonoBehaviour, IAction
    {
        private NavMeshAgent _agent;
        private CharacterCombatTarget _lastCombatTarget;
        private ActionScheduler _actionScheduler;
        private AIAnimation _aiAnimation;
        private CharacterStats _characterStats;

        private bool _isAttacking;

        private float _timeElapsedSinceLastAttack = Mathf.Infinity;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _agent = GetComponent<NavMeshAgent>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _aiAnimation = GetComponent<AIAnimation>();
            _characterStats = GetComponent<CharacterStats>();
        }

        private void Update()
        {
            _timeElapsedSinceLastAttack += Time.deltaTime;
            AttackBehaviour();
        }

        public void Attack(CharacterCombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _lastCombatTarget = combatTarget;
            _isAttacking = true;
            AttackBehaviour();
        }

        private void AttackBehaviour()
        {
            if (!_isAttacking) return;
            
            if (!GetAgentInRange())
            {
                MoveToDestinationInCombat(_lastCombatTarget.transform.position);
                return;
            }
            
            if (_lastCombatTarget.GetComponent<CharacterHealth>().IsDead())
            {
                CancelAction();
                return;
            };
            
            transform.LookAt(_lastCombatTarget.transform);
            
            if (_timeElapsedSinceLastAttack >= _characterStats.currentTimeBetweenAttacks)
            {
                _timeElapsedSinceLastAttack = 0f;
                _aiAnimation.ResetTriggerAnimation(ProjectConstants.AnimationCancelAttack);
                _aiAnimation.TriggerAnimation(ProjectConstants.AnimationAttack, _characterStats.currentAttackRate);
            }
        }
        
        private bool GetAgentInRange()
        {
            if (_agent.remainingDistance < 0.01f) return false;
            
            var inRange = _agent.remainingDistance <  _characterStats.currentAttackRange;
            return inRange;
        }
        
        private void MoveToDestinationInCombat(Vector3 destination)
        {
            _agent.stoppingDistance = _characterStats.currentAttackRange;
            _agent.speed = _characterStats.currentMaxMovementSpeed;
            _agent.SetDestination(destination);
        }

        public void CancelAction()
        {
            _isAttacking = false;
            _aiAnimation.ResetTriggerAnimation(ProjectConstants.AnimationAttack);
            _aiAnimation.TriggerAnimation(ProjectConstants.AnimationCancelAttack);
        }

        //ANIMATION EVENT
        private void Hit()
        {
            if(!_lastCombatTarget) return;
            _lastCombatTarget.TakeDamage(_characterStats.currentAttackDamage);
        }
    }
}