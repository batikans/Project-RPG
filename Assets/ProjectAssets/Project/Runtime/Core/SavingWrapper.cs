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
            Initialize();
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
