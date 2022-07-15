using UnityEngine;

namespace ProjectAssets.Project.Runtime.Character.Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private float turnSmoothTime = 0.1f;
        
        private CharacterController _characterController;
        private CharacterStats _characterStats;

        private float _movementSpeed;
        private float _currentSpeed;
        private float _turnSmoothVelocity;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _characterStats = GetComponent<CharacterStats>();
        }

        private void Start()
        {
            GetCurrentMovementSpeed();
        }

        public void MovePlayer(Vector2 inputVector, bool inCombat)
        {
            if (!inCombat)
            {
                var horizontal = inputVector.x;
                var vertical = inputVector.y;
            
                if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
                {
                    var direction = new Vector3(horizontal, 0f, vertical).normalized;

                    var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                        turnSmoothTime);
                    transform.rotation = Quaternion.Euler(0f, angle, 0f);
                
                    _characterController.Move(direction * _movementSpeed * Time.deltaTime);
                    _currentSpeed = _characterController.velocity.magnitude;
                }
                else
                {
                    _currentSpeed = 0f;
                }
            }
            else
            {
                _currentSpeed = 0f;
            }
        }

        private void GetCurrentMovementSpeed()
        {
            _movementSpeed = _characterStats.GetMovementSpeed();
        }

        public float GetPlayerVelocity()
        {
            return _currentSpeed;
        }
    }
}
