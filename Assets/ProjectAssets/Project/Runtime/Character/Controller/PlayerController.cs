using Project.Scripts.Runtime.Character;
using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Controller
{
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterCombat))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Cache")] 
        [SerializeField] private Camera gameCamera;

        private CharacterMovement _characterMovement;
        private CharacterCombat _characterCombat;
        private CharacterCombatTarget _characterCombatTarget;
        
        [Header("Settings")] 
        [SerializeField] private LayerMask terrainLayer;

        private bool _canControl = true;
        
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnSentInputInfo, PlayerActionPriority);
            EventManager.StartListening(ProjectConstants.OnCharacterDead, DisableControl);
            EventManager.StartListening(ProjectConstants.OnCinematicStarted, DisableControl);
            EventManager.StartListening(ProjectConstants.OnCinematicFinished, EnableControl);
            
            Initialize();
        }

        private void Initialize()
        {
            _characterMovement = GetComponent<CharacterMovement>();
            _characterCombat = GetComponent<CharacterCombat>();
            _characterCombatTarget = GetComponent<CharacterCombatTarget>();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnSentInputInfo, PlayerActionPriority);
            EventManager.StopListening(ProjectConstants.OnCharacterDead, DisableControl);
            EventManager.StopListening(ProjectConstants.OnCinematicStarted, DisableControl);
            EventManager.StopListening(ProjectConstants.OnCinematicFinished, EnableControl);
        }

        private void PlayerActionPriority(EventParameters parameters)
        {
            if(!_canControl) return;
            
            if(InteractWithCombat(parameters)) return;
            if(InteractWithMovement(parameters)) return;
            //print("IDLE");
        }
        
        private bool InteractWithMovement(EventParameters parameters)
        {
            if (parameters.InputStateParameter != InputState.MouseHold) return false;
            return MoveToCursor();
        }

        private bool InteractWithCombat(EventParameters parameters)
        {
            if (parameters.InputStateParameter is not (InputState.MouseDown or InputState.MouseHold))
                return false;
            
            var hits = new RaycastHit[3];
            var layerMask = ~0; // Set the layer mask to all layers
                
            var hitCount = Physics.RaycastNonAlloc(GetInputRay(),hits, Mathf.Infinity, layerMask);
                
            for (int i = 0; i < hitCount; i++)
            {
                var combatTarget = hits[i].transform.GetComponent<CharacterCombatTarget>();
                if (!combatTarget || combatTarget == _characterCombatTarget) continue;
                
                var isCombatTargetDead = combatTarget.GetComponent<CharacterHealth>().IsDead();
                if (isCombatTargetDead) continue;

                if (parameters.InputStateParameter is (InputState.MouseDown or InputState.MouseHold))
                {
                    _characterCombat.Attack(combatTarget);
                }
                return true;
            }
            return false;
        }

        private bool MoveToCursor()
        {
            if (Physics.Raycast(GetInputRay(), out var hit, terrainLayer))
            {
                var destination = hit.point;
                _characterMovement.MoveToDestination(destination, 1f);
                return true;
            }
            return false;
        }

        private Ray GetInputRay()
        {
            var inputRay = gameCamera.ScreenPointToRay(Input.mousePosition);
            return inputRay;
        }

        private void DisableControl(EventParameters parameters)
        {
            if (parameters.GameObjectParameter != null && parameters.GameObjectParameter != gameObject) return;
            
            _canControl = false;
            _characterCombat.CancelAction();
            _characterMovement.CancelAction();
            _characterMovement.DisableAgent();
        }

        private void EnableControl(EventParameters eventParameters)
        {
            _canControl = true;
            _characterMovement.EnableAgent();
        }
    }
}
