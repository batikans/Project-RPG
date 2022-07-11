using ProjectAssets.Project.Runtime.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace ProjectAssets.Project.Runtime.CameraAndCinematics
{
    [RequireComponent(typeof(PlayableDirector))]
    public class CinematicTrigger : MonoBehaviour
    {
        private PlayableDirector _playableDirector;

        private bool _isCinematicActivated;

        private void Awake()
        {
            Initialize();
        }
        
        private void Initialize()
        {
            _playableDirector = GetComponent<PlayableDirector>();
            _playableDirector.playOnAwake = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isCinematicActivated && other.CompareTag(ProjectConstants.TagPlayer))
            {
                _isCinematicActivated = true;
                _playableDirector.Play();
            }
        }
    }
}
