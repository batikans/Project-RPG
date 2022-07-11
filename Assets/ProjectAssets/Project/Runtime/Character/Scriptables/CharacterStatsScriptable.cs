using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Scriptables
{
    [CreateAssetMenu(fileName = "CharacterStats", menuName = "ScriptableObjects/CharacterStats", order = 1)]
    public class CharacterStatsScriptable : ScriptableObject
    {
        [Header("Health")]
        public float health;
        
        [Header("Movement")]
        public float maxMovementSpeed;
        [Range(0, 1)] public float movementSpeedFraction;

        [Header("Combat")] 
        public float attackDamage;
        public float attackRange;
        public float attackRate;
        public float timeBetweenAttacks;
    }
}

