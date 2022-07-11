using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ProjectAssets.Project.Editor
{
    public class BuildSettingsEditor : EditorWindow
    {
        private readonly List<SceneAsset> _sceneAssets = new List<SceneAsset>();

        // Add menu item named "Example Window" to the Window menu
        [MenuItem("Window/MyWindows/BuildSettingsEditor", false, 1)]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(BuildSettingsEditor));
        }

        void OnGUI()
        {
            GUILayout.Label("Scenes to include in build:", EditorStyles.boldLabel);
            for (int i = 0; i < _sceneAssets.Count; ++i)
            {
                _sceneAssets[i] = (SceneAsset)EditorGUILayout.ObjectField(_sceneAssets[i], typeof(SceneAsset), false);
            }
            if (GUILayout.Button("Add"))
            {
                _sceneAssets.Add(null);
            }

            GUILayout.Space(8);

            if (GUILayout.Button("Apply To Build Settings"))
            {
                SetEditorBuildSettingsScenes();
            }
        }

        private void SetEditorBuildSettingsScenes()
        {
            // Find valid Scene paths and make a list of EditorBuildSettingsScene
            List<EditorBuildSettingsScene> editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            foreach (var sceneAsset in _sceneAssets)
            {
                string scenePath = AssetDatabase.GetAssetPath(sceneAsset);
                if (!string.IsNullOrEmpty(scenePath))
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            }

            // Set the Build Settings window Scene list
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }
    }
}