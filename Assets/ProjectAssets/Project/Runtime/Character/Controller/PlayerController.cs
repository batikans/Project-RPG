using ProjectAssets.Project.Runtime.Character.Player;
using ProjectAssets.Project.Runtime.Core;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectAssets.Project.Runtime.Character.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private PlayerCombat _playerCombat;
        private PlayerInputActions _playerInputActions;

        private bool _canControl = true;
        private bool _inCombat;

        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnCharacterDead, DisableControlAfterDeath);
            EventManager.StartListening(ProjectConstants.OnPlayerAttackStarted, AttackStarted);
            EventManager.StartListening(ProjectConstants.OnPlayerAttackFinished, AttackFinished);
            EventManager.StartListening(ProjectConstants.OnGameStateChanged, ToggleControl);
            
            Initialize();
        }

        private void Initialize()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _playerCombat = GetComponent<PlayerCombat>();
            
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.Player.Enable();
            _playerInputActions.Player.Combat.performed += CombatBehaviour;
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnCharacterDead, DisableControlAfterDeath);
            EventManager.StopListening(ProjectConstants.OnPlayerAttackStarted, AttackStarted);
            EventManager.StopListening(ProjectConstants.OnPlayerAttackFinished, AttackFinished);
            EventManager.StopListening(ProjectConstants.OnGameStateChanged, ToggleControl);
            
            _playerInputActions.Player.Combat.performed -= CombatBehaviour;
            _playerInputActions.Player.Disable();
        }

        private void Update()
        {
            if (!_canControl) return;
            
            MovementBehaviour();
        }
        
        private void MovementBehaviour()
        {
            var inputVector = _playerInputActions.Player.Movement.ReadValue<Vector2>();
            _playerMovement.MovePlayer(inputVector, _inCombat);
        }
        
        private void CombatBehaviour(InputAction.CallbackContext context)
        {
            if (!_canControl || _inCombat) return;

            _playerCombat.Attack();
        }

        private void DisableControlAfterDeath(EventParameters eventParameters)
        {
            if (eventParameters.CharacterGameObject != null && eventParameters.CharacterGameObject != gameObject) return;

            _canControl = false;
            _playerInputActions.Player.Disable();
        }

        private void ToggleControl(EventParameters eventParameters)
        {
            var gameStateToUse = eventParameters.GameState;

            if (gameStateToUse == GameState.SettingsMenu)
            {
                _canControl = false;
            }
            else if (gameStateToUse == GameState.Gameplay)
            {
                _canControl = true;
            }
        }

        private void AttackStarted(EventParameters eventParameters)
        {
            _inCombat = true;
        }
        
        private void AttackFinished(EventParameters eventParameters)
        {
            _inCombat = false;
        }
    }
}
