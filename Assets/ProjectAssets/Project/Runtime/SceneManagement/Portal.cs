using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private SceneName sceneName;

        private EventParameters _eventParameters;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ProjectConstants.TagPlayer)) return;
            
            _eventParameters = new EventParameters()
            {
                SceneName = sceneName
            };
                
            EventManager.TriggerEvent(ProjectConstants.OnSentSceneNameInfo, _eventParameters);
            print("Teleport!");
        }
    }
}
