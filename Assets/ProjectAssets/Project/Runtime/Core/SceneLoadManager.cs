using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectAssets.Project.Runtime.Core
{
    public class SceneLoadManager : MonoBehaviour
    {
        private SceneName _sceneNameToUse;
        private string _sceneNameString;
        
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnSentSceneNameInfo, LoadScene);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnSentSceneNameInfo, LoadScene);
        }
        
        private void LoadScene(EventParameters eventParameters)
        {
            print("scene load");
            _sceneNameToUse = eventParameters.SceneName;
            
            switch (_sceneNameToUse)
            {
                case SceneName.Level01:
                    _sceneNameString = ProjectConstants.SceneLevel01;
                    break;
                case SceneName.Level02:
                    _sceneNameString = ProjectConstants.SceneLevel02;
                    break;
            }

            SceneManager.LoadScene(_sceneNameString);
        }
    }

    public enum SceneName
    {
        Level01,
        Level02
    }
}
