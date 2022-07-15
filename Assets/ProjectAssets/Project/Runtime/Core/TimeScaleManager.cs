using UnityEngine;

namespace ProjectAssets.Project.Runtime.Core
{
    public class TimeScaleManager : MonoBehaviour
    {
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnGameStateChanged, TogglePauseTime);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnGameStateChanged, TogglePauseTime);
        }

        private void TogglePauseTime(EventParameters eventParameters)
        {
            var gameStateToUse = eventParameters.GameState;

            if (gameStateToUse == GameState.SettingsMenu)
            {
                Time.timeScale = 0f;
            }
            else if (gameStateToUse == GameState.Gameplay)
            {
                Time.timeScale = 1f;
            }
        }
    }
}
