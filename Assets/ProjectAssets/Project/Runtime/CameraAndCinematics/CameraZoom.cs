using Cinemachine;
using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.CameraAndCinematics
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class CameraZoom : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float minDistance = 5f;
        [SerializeField] private float maxDistance = 20f;
        [SerializeField] private float zoomSpeed = 12f;

        private CinemachineVirtualCamera _virtualCamera;
        private CinemachineComponentBase _componentBase;
        
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnSentInputInfo, ZoomBehaviour);
            Initialize();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnSentInputInfo, ZoomBehaviour);
        }

        private void Initialize()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _componentBase = _virtualCamera.GetCinemachineComponent<CinemachineComponentBase>();
        }

        private void ZoomBehaviour(EventParameters eventParameters)
        {
            if (eventParameters.InputStateParameter is not (InputState.UpArrow or InputState.DownArrow)) return;

            if (_componentBase is not CinemachineFramingTransposer) return;
            
            var framingTransposer = _componentBase as CinemachineFramingTransposer;
            var valueToUse = framingTransposer.m_CameraDistance;   
            
            if (eventParameters.InputStateParameter == InputState.UpArrow)
            {
                if (valueToUse >= maxDistance)
                {
                    valueToUse = maxDistance;
                }
                else
                {
                    valueToUse += zoomSpeed * Time.deltaTime;
                }
            }
            else
            {
                if (valueToUse <= minDistance)
                {
                    valueToUse = minDistance;
                }
                else
                {
                    valueToUse -= zoomSpeed * Time.deltaTime;
                }
            }

            framingTransposer.m_CameraDistance = valueToUse;
        }
    }
}
