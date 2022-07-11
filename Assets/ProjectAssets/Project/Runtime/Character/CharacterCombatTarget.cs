using Project.Scripts.Runtime.Character;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character
{
    [RequireComponent(typeof(CharacterHealth))]
    public class CharacterCombatTarget : MonoBehaviour
    {
        private CharacterHealth _characterHealth;

        private void Awake()
        {
            _characterHealth = GetComponent<CharacterHealth>();
        }

        public void TakeDamage(float damageAmount)
        {
            _characterHealth.DecreaseHealth(damageAmount);
        }
    }
}