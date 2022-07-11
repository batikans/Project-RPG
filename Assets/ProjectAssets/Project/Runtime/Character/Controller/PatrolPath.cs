using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        private int _waypointCount;

        private void Start()
        {
            _waypointCount = transform.childCount;
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.GetChild(i).position,0.3f);
                var targetPosition = i != transform.childCount - 1 ? GetWaypoint(i + 1) : GetWaypoint(0);
                Gizmos.DrawLine(GetWaypoint(i), targetPosition);
            }
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int GetNextIndex(int currentIndex)
        {
            var indexToUse = currentIndex + 1;

            if (indexToUse == _waypointCount)
            {
                indexToUse = 0;
            }

            return indexToUse;
        }
    }
}
