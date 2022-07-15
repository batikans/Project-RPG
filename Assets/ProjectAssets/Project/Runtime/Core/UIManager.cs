using UnityEngine;
using UnityEngine.InputSystem;

namespace ProjectAssets.Project.Runtime.Core
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject settingsCanvas;

        private PlayerInputActions _playerInputActions;

        private EventParameters _eventParameters;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _playerInputActions = new PlayerInputActions();
            _playerInputActions.GameMenu.Enable();
            _playerInputActions.GameMenu.ToggleMenu.performed += ToggleSettings;
            
            settingsCanvas.SetActive(false);
        }

        private void OnDestroy()
        {
            _playerInputActions.GameMenu.ToggleMenu.performed -= ToggleSettings;
            _playerInputActions.GameMenu.Disable();
        }

        private void ToggleSettings(InputAction.CallbackContext context)
        {
            if (!settingsCanvas.activeInHierarchy)
            {
                settingsCanvas.SetActive(true);
                GameManager.Instance.UpdateGameState(GameState.SettingsMenu);
            }
            else
            {
                settingsCanvas.SetActive(false);
                GameManager.Instance.UpdateGameState(GameState.Gameplay);
            }
        }
    }
}
