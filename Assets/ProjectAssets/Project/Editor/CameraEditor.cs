using ProjectAssets.Project.Runtime.CameraAndCinematics;
using UnityEditor;

namespace ProjectAssets.Project.Editor
{
    [CustomEditor(typeof(CameraFollower))]
    public class CameraEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            CameraFollower cameraFollower = (CameraFollower)target;
            cameraFollower.UpdateTransform();
        }
    }
}
