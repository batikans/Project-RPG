using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private SceneName sceneName;
        [SerializeField] private Transform playerSpawnTransform;

        private bool _isPortalActivated;
        
        private EventParameters _eventParameters = new EventParameters();
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(ProjectConstants.TagPlayer) || _isPortalActivated) return;

            _isPortalActivated = true;
            _eventParameters = new EventParameters()
            {
                SceneName = sceneName,
                PlayerSpawnPosition = playerSpawnTransform.position,
                IsTeleporting = _isPortalActivated
            };
                
            EventManager.TriggerEvent(ProjectConstants.OnSentSceneNameInfo, _eventParameters);
        }
    }
}
