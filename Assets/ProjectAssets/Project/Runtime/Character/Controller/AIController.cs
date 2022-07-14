using ProjectAssets.Project.Runtime.Character.AI;
using ProjectAssets.Project.Runtime.Core;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Controller
{
    [RequireComponent(typeof(CharacterStats))]
    [RequireComponent(typeof(AIStats))]
    [RequireComponent(typeof(AIMovement))]
    [RequireComponent(typeof(AICombat))]
    [RequireComponent(typeof(ActionScheduler))]
    public class AIController : MonoBehaviour
    {
        [Header("PatrolSettings")] 
        [SerializeField] private PatrolPath patrolPath;
        
        private CharacterStats _characterStats;
        private AIStats _aiStats;
        private AIMovement _aiMovement;
        private AICombat _aiCombat;

        private Transform _playerTransform;
        private CharacterCombatTarget _playerCombatTarget;
        private ActionScheduler _actionScheduler;

        private float _distanceToPlayer;
        private float _stoppingDistance;
        private float _distanceToNextWaypointPosition;
        private float _timeSinceLastSawPlayer = Mathf.Infinity;
        private float _timeSinceAtLastWaypoint = Mathf.Infinity;

        private int _currentWaypointIndex = 0;

        private Vector3 _guardPosition;
        private Vector3 _nextWaypointPosition;

        private bool _canControl = true;
        
        private void Awake()
        {
            EventManager.StartListening(ProjectConstants.OnCharacterDead, DisableControl);
            
            Initialize();
        }

        private void Initialize()
        {
            _characterStats = GetComponent<CharacterStats>();
            _aiStats = GetComponent<AIStats>();
            _aiMovement = GetComponent<AIMovement>();
            _aiCombat = GetComponent<AICombat>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _playerTransform = GameObject.FindGameObjectWithTag(ProjectConstants.TagPlayer).transform;
            _playerCombatTarget = _playerTransform.GetComponent<CharacterCombatTarget>();
        }

        private void OnDestroy()
        {
            EventManager.StopListening(ProjectConstants.OnCharacterDead, DisableControl);
        }

        private void Start()
        {
            _guardPosition = transform.position;
        }

        private void Update()
        {
            if (!_canControl) return;
            
            if (IsPlayerInRange())
            {
                AttackBehaviour();
            }
            
            else if (_timeSinceLastSawPlayer < _aiStats.suspicionDuration)
            {
                SuspicionBehaviour();
            }
            
            else
            {
                PatrolBehaviour();
            }

            AITimer();
        }

        private void AITimer()
        {
            _timeSinceLastSawPlayer += Time.deltaTime;
            _timeSinceAtLastWaypoint += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            _timeSinceLastSawPlayer = 0f;
            _aiCombat.Attack(_playerCombatTarget);
        }

        private void SuspicionBehaviour()
        {
            _actionScheduler.CancelAnyAction();
        }

        private void PatrolBehaviour()
        {
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    _timeSinceAtLastWaypoint = 0f;
                    CycleWaypoint();
                }
                _nextWaypointPosition = GetCurrentWaypoint();
            }
            else
            {
                _nextWaypointPosition = _guardPosition;
            }

            if (_timeSinceAtLastWaypoint > _aiStats.waypointDwellingDuration)
            {
                _aiMovement.MoveToDestination(_nextWaypointPosition,_characterStats.currentMovementSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            _distanceToNextWaypointPosition = Vector3.Distance(_nextWaypointPosition, transform.position);
            return _distanceToNextWaypointPosition < _aiStats.waypointTolerance;
        }
        
        private void CycleWaypoint()
        {
            _currentWaypointIndex = patrolPath.GetNextIndex(_currentWaypointIndex);
        }
        
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(_currentWaypointIndex);
        }

        private bool IsPlayerInRange()
        {
            _distanceToPlayer = Vector3.Distance(_playerTransform.transform.position, transform.position);
            return _distanceToPlayer < _aiStats.chaseDistance;
        }
        
        private void DisableControl(EventParameters parameters)
        {
            if (parameters.CharacterGameObject != gameObject) return;
            _canControl = false;
            _aiCombat.CancelAction();
            _aiMovement.CancelAction();
            _aiMovement.DisableAgent();
        }
    }
}
