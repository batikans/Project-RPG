using ProjectAssets.Project.Runtime.Core;
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
        private Animator _animator;
        
        private float _turnSmoothVelocity;
        private float _inputMagnitude;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _characterStats = GetComponent<CharacterStats>();
        }

        private void Start()
        {
            UpdateMovementSpeed(_characterStats.currentMaxMovementSpeed);
        }

        public void MovePlayer(Vector2 inputVector, bool inCombat)
        {
            var horizontal = inputVector.x;
            var vertical = inputVector.y;

             var direction = new Vector3(horizontal, 0f, vertical);
            _inputMagnitude = Mathf.Clamp01(direction.magnitude);
            direction.Normalize();
            
            if (!inCombat && Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f)
            {
                var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);   
            }
        }

        private void OnAnimatorMove()
        {
            var velocity = _animator.deltaPosition;
            _characterController.Move(velocity);
        }

        public float GetInputMagnitude()
        {
            return _inputMagnitude;
        }

        private void UpdateMovementSpeed(float movementSpeed)
        {
            _animator.SetFloat(ProjectConstants.AnimationMovementSpeed, movementSpeed);
        }
    }
}
