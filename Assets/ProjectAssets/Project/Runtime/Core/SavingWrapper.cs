using ProjectAssets.Project.Runtime.Saving;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Core
{
    [RequireComponent(typeof(SavingSystem))]
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem _savingSystem;
        private InputState _inputState = InputState.Null;
        
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnSentInputInfo, SaveAndLoad);
            
            Initialize();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnSentInputInfo, SaveAndLoad);
        }

        private void Initialize()
        {
            _savingSystem = GetComponent<SavingSystem>();
        }
        
        private void SaveAndLoad(EventParameters eventParameters)
        {
            _inputState = eventParameters.InputState;
            if (_inputState is not (InputState.SDown or InputState.LDown)) return;

            if (_inputState == InputState.SDown)
            {
                Save();
            }
            else if (_inputState == InputState.LDown)
            {
                Load();
            }
        }

        private void Save()
        {
            _savingSystem.Save(ProjectConstants.SaveFile);
        }

        private void Load()
        {
            _savingSystem.Load(ProjectConstants.SaveFile);
        }
    }
}
