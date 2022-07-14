using ProjectAssets.Project.Runtime.Character.Scriptables;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("Cache")]
        [SerializeField] private CharacterStatsScriptable characterDefaultStats;

        [Header("Health")] 
        [SerializeField] internal float currentHealth;
        
        [Header("Movement")]
        [SerializeField] internal float currentMaxMovementSpeed;
        [Range(0f,1f)][SerializeField] internal float currentMovementSpeedFraction;
        
        [Header("Combat")]
        [SerializeField] internal float currentAttackDamage;
        [SerializeField] internal float currentAttackRange;
        [SerializeField] internal float currentAttackRate;
        [SerializeField] internal float currentTimeBetweenAttacks;
        
        private void Awake()
        {
            SetupCharacterStats();
        }

        private void SetupCharacterStats()
        {
            currentHealth = characterDefaultStats.health;
            
            currentMaxMovementSpeed = characterDefaultStats.maxMovementSpeed;
            currentMovementSpeedFraction = characterDefaultStats.movementSpeedFraction;

            currentAttackDamage = characterDefaultStats.attackDamage;
            currentAttackRange = characterDefaultStats.attackRange;
            currentAttackRate = characterDefaultStats.attackRate;
            currentTimeBetweenAttacks = characterDefaultStats.timeBetweenAttacks;
        }

        public float GetMovementSpeed()
        {
            return currentMaxMovementSpeed;
        }
    }
}
