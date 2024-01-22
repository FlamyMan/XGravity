using UnityEngine;

namespace Assets.code
{
    public class PlayerInputControl : MonoBehaviour 
    {
        private PlayerInput _playerInput;

        [SerializeField] private Transform _camera;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private float _lookSpeed;

        private Vector2 _moveDirection;
        private Vector2 _lookDirection;
        
        private void Awake()
        {
            _playerInput = new();
            _playerInput.Player.Jump.started += movement.Jump;
        }

        private void Look()
        {
            float scaledLookSpeed = _lookSpeed * Time.deltaTime;

            Vector3 cameraRotation = new Vector3(-_lookDirection.y, 0f, 0f) * scaledLookSpeed;
            Vector3 objRotation = new Vector3(0f, _lookDirection.x, 0f) * scaledLookSpeed;

            transform.Rotate(objRotation);
            _camera.Rotate(cameraRotation);
        }

        private void Move()
        {
            movement.Move(_moveDirection);
        }

        private void Update()
        {
            _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            _lookDirection = _playerInput.Player.Look.ReadValue<Vector2>();

            Look();
            Move();
        }

        private void OnEnable()
        {
            _playerInput.Enable();

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            _playerInput.Disable();

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
