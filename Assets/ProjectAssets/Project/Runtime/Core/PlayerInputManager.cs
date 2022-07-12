using UnityEngine;

public enum InputState
{
    Null,
    MouseDown,
    MouseUp,
    MouseHold,
    UpArrow,
    DownArrow,
    ESC
}

namespace ProjectAssets.Project.Runtime.Core
{
    public class PlayerInputManager : MonoBehaviour
    {
        private InputState _currentInputState;
        private EventParameters _eventParameters;

        private bool _isInputActive;

        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnSceneStartedLoading, DisableInput);
            EventManager.StartListening(ProjectConstants.OnSceneFinishedLoading, EnableInput);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnSceneStartedLoading, DisableInput);
            EventManager.StopListening(ProjectConstants.OnSceneFinishedLoading, EnableInput);
        }

        private void Update()
        {
            if (!_isInputActive) return;
            
            SentInputInfo();
        }

        private void SentInputInfo()
        {
            _currentInputState = InputState.Null;
            
            if (Input.GetMouseButtonDown(0))
            {
                _currentInputState = InputState.MouseDown;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _currentInputState = InputState.MouseUp;
            }
            else if (Input.GetMouseButton(0))
            {
                _currentInputState = InputState.MouseHold;
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                _currentInputState = InputState.UpArrow;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                _currentInputState = InputState.DownArrow;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                _currentInputState = InputState.ESC;
            }

            _eventParameters.InputState = _currentInputState;
            EventManager.TriggerEvent(ProjectConstants.OnSentInputInfo, _eventParameters);
        }
        
        private void EnableInput(EventParameters eventParameters)
        {
            _isInputActive = true;
        }
        
        private void DisableInput(EventParameters eventParameters)
        {
            _isInputActive = false;
        }
    }
}