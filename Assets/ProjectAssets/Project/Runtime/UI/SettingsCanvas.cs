using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.UI
{
    public class SettingsCanvas : MonoBehaviour
    {
        public void OnPressedSaveGame()
        {
            EventManager.TriggerEvent(ProjectConstants.OnSaveGame);
        }

        public void OnPressedLoadGame()
        {
            EventManager.TriggerEvent(ProjectConstants.OnLoadGame);
        }
        
        public void OnPressedQuitApplication()
        {
            Application.Quit();
        }
    }
}
