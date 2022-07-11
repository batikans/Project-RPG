using UnityEngine;
using UnityEditor;
using UnityEditor.Rendering;

namespace Project.Scripts.Editor
{
    public class ObjectSelectionEditor : EditorWindow
    {
        private GameObject[] _environmentObjectsCache;
        
        [MenuItem("Window/MyWindows/ObjectSelector", false, 0)]
        public static void  ShowWindow ()
        {
            GetWindow<ObjectSelectionEditor>("Object Selector");
        }
    
        void OnGUI ()
        {
            //---CHARACTER SELECTION
            GUILayout.Label("Select characters :");
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Player"))
            {
                var playerObject = GameObject.FindWithTag("Player");
                Selection.objects = new Object[] { playerObject };
                SceneView.FrameLastActiveSceneView();
            }

            if (GUILayout.Button("Enemies"))
            {
                var enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
                Selection.objects = enemyObjects;
                SceneView.FrameLastActiveSceneView();
            }
            GUILayout.EndHorizontal();
            
            //---ENVIRONMENT SELECTION
            GUILayout.Label("Select environment :");
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Buildings"))
            {
                var buildingObjects = GameObject.FindGameObjectsWithTag("Building");
                Selection.objects = buildingObjects;
            }

            if (GUILayout.Button("Boxes"))
            {
                var boxObjects = GameObject.FindGameObjectsWithTag("Box");
                Selection.objects = boxObjects;
            }
            GUILayout.EndHorizontal();
            
            //---ENVIRONMENT SELECTION
            GUILayout.Label("Toggle environment :");
            GUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Environment On/Off "))
            {
                var environmentObjects = GameObject.FindGameObjectsWithTag("Environment");

                if (environmentObjects.Length != 0)
                {
                    _environmentObjectsCache = new GameObject[environmentObjects.Length];
                    _environmentObjectsCache = environmentObjects;
                }
                
                Selection.objects = _environmentObjectsCache;

                foreach (var environmentObject in _environmentObjectsCache)
                {
                    environmentObject.SetActive(!environmentObject.activeInHierarchy);
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
