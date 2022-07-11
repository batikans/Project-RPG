using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character
{
    public class CharacterHealth : MonoBehaviour
    {
        private CharacterStats _characterStats;
        
        private bool _isDead;

        private void Awake()
        {
            _characterStats = GetComponent<CharacterStats>();
        }

        public bool IsDead()
        {
            return _isDead;
        }

        public void DecreaseHealth(float damageAmount)
        {
            _characterStats.currentHealth = Mathf.Max(_characterStats.currentHealth - damageAmount, 0f);

            if (_characterStats.currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            if (_isDead) return;
            
            _isDead = true;

            var eventParameters = new EventParameters()
            {
                GameObjectParameter = gameObject,
                BoolParameter = _isDead
            };
            
            EventManager.TriggerEvent(ProjectConstants.OnCharacterDead, eventParameters);
        }
    }
}
