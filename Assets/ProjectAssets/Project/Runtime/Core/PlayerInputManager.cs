using UnityEngine;

public enum InputState
{
    Null,
    MouseDown,
    MouseUp,
    MouseHold,
    UpArrow,
    DownArrow
}

namespace ProjectAssets.Project.Runtime.Core
{
    public class PlayerInputManager : MonoBehaviour
    {
        private InputState _currentInputState;
        private EventParameters _eventParameters;
        
        private void Update()
        {
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

            _eventParameters.InputStateParameter = _currentInputState;
            EventManager.TriggerEvent(ProjectConstants.OnSentInputInfo, _eventParameters);
        }
    }
}