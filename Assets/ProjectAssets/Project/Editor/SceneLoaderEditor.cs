using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace ProjectAssets.Project.Editor
{
    public class SceneLoaderEditor : EditorWindow
    {
        [MenuItem("Window/MyWindows/SceneLoader", false, 3)]
        public static void  ShowWindow ()
        {
            GetWindow<SceneLoaderEditor>("Scene Loader");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Load Core Scene"))
            {
                var scenePath = "Assets/ProjectAssets/Scenes/Core.unity";
                EditorSceneManager.OpenScene(scenePath);
            }
            if (GUILayout.Button("Load UI Scene"))
            {
                var scenePath = "Assets/ProjectAssets/Scenes/UI.unity";
                EditorSceneManager.OpenScene(scenePath);
            }
            if (GUILayout.Button("Load Level 01"))
            {
                var scenePath = "Assets/ProjectAssets/Scenes/Level 01.unity";
                EditorSceneManager.OpenScene(scenePath);
            }
            if (GUILayout.Button("Load Level 02"))
            {
                var scenePath = "Assets/ProjectAssets/Scenes/Level 02.unity";
                EditorSceneManager.OpenScene(scenePath);
            }
        }
    }
}
