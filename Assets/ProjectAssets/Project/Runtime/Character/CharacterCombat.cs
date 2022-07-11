using ProjectAssets.Project.Runtime.Core;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectAssets.Project.Runtime.Character
{
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(CharacterCombatTarget))]
    [RequireComponent(typeof(ActionScheduler))]
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterCombat : MonoBehaviour, IAction
    {
        private NavMeshAgent _agent;
        private CharacterCombatTarget _lastCombatTarget;
        private ActionScheduler _actionScheduler;
        private CharacterAnimation _characterAnimation;
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
            _characterAnimation = GetComponent<CharacterAnimation>();
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
                _characterAnimation.ResetTriggerAnimation(ProjectConstants.AnimationCancelAttack);
                _characterAnimation.TriggerAnimation(ProjectConstants.AnimationAttack, _characterStats.currentAttackRate);
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
            _characterAnimation.ResetTriggerAnimation(ProjectConstants.AnimationAttack);
            _characterAnimation.TriggerAnimation(ProjectConstants.AnimationCancelAttack);
        }

        //ANIMATION EVENT
        private void Hit()
        {
            if(!_lastCombatTarget) return;
            _lastCombatTarget.TakeDamage(_characterStats.currentAttackDamage);
        }
    }
}