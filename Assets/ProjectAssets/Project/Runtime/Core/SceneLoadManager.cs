using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectAssets.Project.Runtime.Core
{
    public class SceneLoadManager : MonoBehaviour
    {
        private SceneName _sceneNameToUse;
        
        private string _sceneNameToLoad;
        private string _sceneNameToUnload;
        
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnSentSceneNameInfo, LoadScene);
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnSentSceneNameInfo, LoadScene);
        }

        private void Start()
        {
            LoadInitialScenes();
        }

        private void LoadInitialScenes()
        {
            SceneManager.LoadSceneAsync(ProjectConstants.SceneUI,LoadSceneMode.Additive);
            
            var eventPar = new EventParameters()
            {
                SceneName = SceneName.Level01
            };
            LoadScene(eventPar);
        }

        private void LoadScene(EventParameters eventParameters)
        {
            _sceneNameToUse = eventParameters.SceneName;
            
            switch (_sceneNameToUse)
            {
                case SceneName.Level01:
                    _sceneNameToLoad = ProjectConstants.SceneLevel01;
                    break;
                case SceneName.Level02:
                    _sceneNameToLoad = ProjectConstants.SceneLevel02;
                    break;
            }
            
            StartCoroutine(LoadSceneCoroutine(eventParameters));
        }
        
        private IEnumerator LoadSceneCoroutine(EventParameters eventParameters)
        {
            EventManager.TriggerEvent(ProjectConstants.OnSceneStartedLoading, eventParameters);
            print("scene started loading");
            
            if (_sceneNameToUnload != null)
            {
                yield return SceneManager.UnloadSceneAsync(_sceneNameToUnload);
            }
            yield return SceneManager.LoadSceneAsync(_sceneNameToLoad,LoadSceneMode.Additive);
            
            _sceneNameToUnload = _sceneNameToLoad;
            EventManager.TriggerEvent(ProjectConstants.OnSceneFinishedLoading, eventParameters);
            print("scene loaded");
        }
    }

    public enum SceneName
    {
        Null,
        Level01,
        Level02
    }
}
