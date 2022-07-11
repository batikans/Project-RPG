using ProjectAssets.Project.Runtime.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace ProjectAssets.Project.Runtime.CameraAndCinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private PlayableDirector _playableDirector;

        private EventParameters _eventParameters;
        
        private void Awake()
         {
             _playableDirector = GetComponent<PlayableDirector>();
         }

        private void Start()
        {
            _playableDirector.played += DisableControl;
            _playableDirector.stopped += EnableControl;
        }

        private void OnDestroy()
        {
            _playableDirector.played -= DisableControl;
            _playableDirector.stopped -= EnableControl;
        }

        private void EnableControl(PlayableDirector obj)
        {
            EventManager.TriggerEvent(ProjectConstants.OnCinematicFinished, new EventParameters());
        }

        private void DisableControl(PlayableDirector obj)
        {
            EventManager.TriggerEvent(ProjectConstants.OnCinematicStarted, new EventParameters());
        }
    }
}