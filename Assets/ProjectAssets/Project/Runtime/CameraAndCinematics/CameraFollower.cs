using UnityEngine;

namespace ProjectAssets.Project.Runtime.CameraAndCinematics
{
    public class CameraFollower : MonoBehaviour
    {
        [Header("Cache")]
        [SerializeField] private Transform targetTransform;

        [Header("Settings")] 
        [SerializeField] private Vector3 offsetVector;
        [SerializeField] [Range(0f, 0.5f)] private float smoothTime = 0.1f;

        private Vector3 _velocityVector;
        
        private void LateUpdate()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            var targetPosition = targetTransform.position + offsetVector;
            
            transform.position =
                Vector3.SmoothDamp(transform.position, targetPosition, ref _velocityVector, smoothTime);
        }
        
        //FOR EDITOR USE
        public void UpdateTransform()
        {
            transform.position = targetTransform.position + offsetVector;
        }
    }
}
