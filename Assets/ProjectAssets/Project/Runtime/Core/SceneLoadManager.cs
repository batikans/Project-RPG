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
            StartCoroutine(LoadInitialScenes());
        }

        private IEnumerator LoadInitialScenes()
        {
            yield return SceneManager.LoadSceneAsync(ProjectConstants.SceneUI,LoadSceneMode.Additive);
            
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
            yield return SceneTransitionCanvas.FadeOutCoroutine(0.2f);
            
            if (_sceneNameToUnload != null)
            {
                yield return SceneManager.UnloadSceneAsync(_sceneNameToUnload);
            }
            yield return SceneManager.LoadSceneAsync(_sceneNameToLoad,LoadSceneMode.Additive);
            
            _sceneNameToUnload = _sceneNameToLoad;
            EventManager.TriggerEvent(ProjectConstants.OnSceneFinishedLoading, eventParameters);
            yield return new WaitForSeconds(1f);
            yield return SceneTransitionCanvas.FadeInCoroutine(0.3f);
        }
    }

    public enum SceneName
    {
        Null,
        Level01,
        Level02
    }
}
