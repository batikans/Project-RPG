using System;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Core
{
    public enum GameState
    {
        GameMenu,
        Cutscene,
        Tutorial,
        Gameplay,
        SettingsMenu,
        Loading
    }
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        private EventParameters _eventParameters;
        private GameState _gameState;
        
        private void Awake()
        {
            Instance = this;
        }

        public void UpdateGameState(GameState newGameState)
        {
            _gameState = newGameState;

            switch (newGameState)
            {
                case GameState.GameMenu:
                    break;
                case GameState.Cutscene:
                    break;
                case GameState.Tutorial:
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.SettingsMenu:
                    break;
                case GameState.Loading:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
            }

            _eventParameters.GameState = _gameState;
            EventManager.TriggerEvent(ProjectConstants.OnGameStateChanged, _eventParameters);
        }
    }
    
    
}