using UnityEngine;

namespace Project.Scripts.Runtime.Character.Scriptables
{
    [CreateAssetMenu(fileName = "AIStats", menuName = "ScriptableObjects/AIStats", order = 2)]
    public class AIStatsScriptable : ScriptableObject
    {
        [Header("PatrolSettings")] 
        public float chaseDistance = 5f;
        public float suspicionDuration = 3f;
        public float waypointTolerance = 1f;
        public float waypointDwellingDuration = 3f;
    }
}
