using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character
{
    public class ActionScheduler : MonoBehaviour
    {
        private IAction _previousAction;

        public void StartAction(IAction action)
        {
            if (_previousAction == action) return;

            _previousAction?.CancelAction();
            _previousAction = action;
        }

        public void CancelAnyAction()
        {
            _previousAction?.CancelAction();
            _previousAction = null;
        }
    }
}
