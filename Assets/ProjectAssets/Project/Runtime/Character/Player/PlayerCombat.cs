using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        private PlayerAnimation _playerAnimation;

        private void Awake()
        {
            _playerAnimation = GetComponent<PlayerAnimation>();
        }

        public void Attack()
        {
            _playerAnimation.TriggerAnimation(ProjectConstants.AnimationAttack); //attack speed can be added here
        }
    }
}
