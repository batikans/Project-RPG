using ProjectAssets.Project.Runtime.Character.AI;
using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private CharacterCombatTarget _lastCombatTarget;
        //private ActionScheduler _actionScheduler;
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
            //_actionScheduler = GetComponent<ActionScheduler>();
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
            //_actionScheduler.StartAction(this);
            _lastCombatTarget = combatTarget;
            _isAttacking = true;
            AttackBehaviour();
        }

        private void AttackBehaviour()
        {
            if (!_isAttacking) return;
            
            if (_timeElapsedSinceLastAttack >= _characterStats.currentTimeBetweenAttacks)
            {
                _timeElapsedSinceLastAttack = 0f;
                _aiAnimation.ResetTriggerAnimation(ProjectConstants.AnimationCancelAttack);
                _aiAnimation.TriggerAnimation(ProjectConstants.AnimationAttack, _characterStats.currentAttackRate);
            }
        }

        // public void CancelAction()
        // {
        //     _isAttacking = false;
        //     _characterAnimation.ResetTriggerAnimation(ProjectConstants.AnimationAttack);
        //     _characterAnimation.TriggerAnimation(ProjectConstants.AnimationCancelAttack);
        // }

        //ANIMATION EVENT
        private void Hit()
        {
            if(!_lastCombatTarget) return;
            _lastCombatTarget.TakeDamage(_characterStats.currentAttackDamage);
        }
    }
}
