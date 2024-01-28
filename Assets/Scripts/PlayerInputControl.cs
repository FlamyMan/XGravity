using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class PlayerInputControl : MonoBehaviour 
    {
        private PlayerInput _playerInput;

        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerMovement movement;
        [SerializeField] private float _lookSpeed;
        [SerializeField] private GameObject _pauseMenu;

        private bool _pauseMode = false;
        private Vector2 _moveDirection;
        private Vector2 _lookDirection;
        
        private void Awake()
        {
            _playerInput = new();
            _playerInput.Player.Jump.started += movement.Jump;
            _playerInput.Player.Interact.started += OnInteraction;
            _playerInput.Player.Pause.started += OnPause;
        }

        private void OnPause(InputAction.CallbackContext context)
        {
            _pauseMode = !_pauseMode;
            _pauseMenu.SetActive(_pauseMode);
            switch (_pauseMode)
            {   
                case true:
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case false:
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
            }
        }

        private void Look()
        {
            float scaledLookSpeed = _lookSpeed * Time.deltaTime;

            Vector3 cameraRotation = new Vector3(-_lookDirection.y, 0f, 0f) * scaledLookSpeed;
            Vector3 objRotation = new Vector3(0f, _lookDirection.x, 0f) * scaledLookSpeed;

            transform.Rotate(objRotation);
            _camera.transform.Rotate(cameraRotation);
        }

        private void Move()
        {
            movement.Move(_moveDirection);
        }

        private void Update()
        {
            _moveDirection = _playerInput.Player.Move.ReadValue<Vector2>();
            _lookDirection = _playerInput.Player.Look.ReadValue<Vector2>();
            if (!_pauseMode)
            {
                Look();
                Move();
            }
        }

        private void OnInteraction(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(new Vector2(_camera.scaledPixelWidth, _camera.scaledPixelHeight) * 0.5f);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                GameObject obj = hit.collider.gameObject;
                IInteractable interactable;
                if (obj.TryGetComponent(out interactable))
                {
                    interactable.Interact();
                }
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.green, float.PositiveInfinity);
            }
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
