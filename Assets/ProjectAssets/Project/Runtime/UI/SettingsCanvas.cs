using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.UI
{
    public class SettingsCanvas : MonoBehaviour
    {
        //[SerializeField] private GameObject settingsObject;

        // private void Awake()
        // {
        //     EventManager.StartListening(ProjectConstants.OnSentInputInfo, ToggleSettingsMenu);
        // }
        //
        // private void OnDestroy()
        // {
        //     EventManager.StopListening(ProjectConstants.OnSentInputInfo, ToggleSettingsMenu);
        // }

        // private void ToggleSettingsMenu(EventParameters eventParameters)
        // {
        //     if (eventParameters.InputState != InputState.EscDown) return;
        //
        //     settingsObject.SetActive(!settingsObject.activeInHierarchy);
        // }
        
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
