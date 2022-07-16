using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private PlayerAnimation _playerAnimation;
        private Animator _animator;
        private CharacterStats _characterStats;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
            _animator = GetComponent<Animator>();
            _characterStats = GetComponent<CharacterStats>();
        }

        private void Start()
        {
            UpdateAttackSpeed(_characterStats.currentAttackRate);
        }

        public void Attack()
        {
            _playerAnimation.TriggerAnimation(ProjectConstants.AnimationAttack);
        }

        private void UpdateAttackSpeed(float attackSpeed)
        {
            _animator.SetFloat(ProjectConstants.AnimationAttackSpeed, attackSpeed);
        }
    }
}
