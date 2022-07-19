using System;
using ProjectAssets.Project.Runtime.Saving;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Core
{
    [RequireComponent(typeof(SavingSystem))]
    public class SavingWrapper : MonoBehaviour
    {
        private SavingSystem _savingSystem;

        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnSaveGame, Save);
            EventManager.StartListening(ProjectConstants.OnLoadGame, Load);
            Initialize();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnSaveGame, Save);
            EventManager.StopListening(ProjectConstants.OnLoadGame, Load);
        }

        private void Initialize()
        {
            _savingSystem = GetComponent<SavingSystem>();
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
