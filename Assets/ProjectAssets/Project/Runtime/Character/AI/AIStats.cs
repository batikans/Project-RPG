using ProjectAssets.Project.Runtime.Character.Scriptables;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.AI
{
    public class AIStats : MonoBehaviour
    {
        [Header("Cache")]
        [SerializeField] internal AIStatsScriptable aiStats;
        
        [Header("PatrolSettings")] 
        public float chaseDistance;
        public float suspicionDuration;
        public float waypointTolerance;
        public float waypointDwellingDuration;
        
        private void Awake()
        {
            SetupAIStats();
        }

        private void SetupAIStats()
        {
            chaseDistance = aiStats.chaseDistance;
            suspicionDuration = aiStats.suspicionDuration;
            waypointTolerance = aiStats.waypointTolerance;
            waypointDwellingDuration = aiStats.waypointDwellingDuration;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
