using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string Climbable = "Climbable";
        public enum State
        {
            walk,
            climb
        }

        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _climbSpeed;
        [SerializeField] private float _jumpForce;

        private CapsuleCollider _playerCollider;
        private Rigidbody rb;
        private Vector3 _normal;
        private Vector3 _lastPosition;
        private State _currentState = State.walk;
        private bool _WasWalking;

        public event Action WalkStarted;
        public event Action WalkEnded;

        public State CurrentState
        {
            get { return _currentState; }
            private set { ChangeState(value); }
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            _playerCollider = rb.GetComponent<CapsuleCollider>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 point = collision.GetContact(0).point;
            if (!collision.gameObject.CompareTag(nameof(Climbable))) return;
            _normal = collision.GetContact(0).normal;
            if ((_normal - transform.up).sqrMagnitude <= 0.3) return;
            CurrentState = State.climb;
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.CompareTag(nameof(Climbable))) return;
            CurrentState = State.walk;
        }

        private void Update()
        {
            if((transform.position - _lastPosition).sqrMagnitude <= 0.0001f || !IsOnGround())
            {
                WalkEnded?.Invoke();
                _WasWalking = false;
                return;
            }

            if (transform.position != _lastPosition)
            {
                if (!_WasWalking)
                {
                    _WasWalking = true;
                    WalkStarted?.Invoke();
                }
            }
            _lastPosition = transform.position;
        }

        private void ChangeState(State newState)
        {
            switch (newState)
            {
                case State.walk:
                    rb.useGravity = true;
                    _currentState = State.walk;
                    break;
                case State.climb:
                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    _currentState = State.climb;
                    break;
                default:
                    break;
            }
        }
        

        public void Jump(InputAction.CallbackContext context)
        {
            switch (CurrentState)
            {
                case State.walk:
                    GroundJump();
                    return;
                case State.climb:
                    WallJump();
                    return;
                default:
                    break;
            }

        }

        private void GroundJump()
        {
            if (IsOnGround())
            {
                rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        private void WallJump()
        {
            rb.AddForce(_normal * _jumpForce, ForceMode.Impulse);
        }

        private bool IsOnGround()
        {
            Vector3 origin = _playerCollider.ClosestPoint(transform.position - new Vector3(0, _playerCollider.height, 0)) + Vector3.up * 0.1f;
            Ray ray = new(origin, Vector3.down);
            if (Physics.Raycast(ray, 0.2f))
            {
                return true;
            }
            return false;
        }

        public void Move(Vector3 input)
        {
            switch (CurrentState)
            {
                case State.walk:
                    WalkMove(input);
                    return;
                case State.climb:
                    ClimbMove(input);
                    return;
                default:
                    break;
            }
        }

        private void ClimbMove(Vector3 input)
        {
            if (CurrentState != State.climb) throw new Exception("The Unit cant climb right now!");

            Vector3 xDirection = new(-_normal.z, 0, _normal.x);
            float scaledSpeed = _climbSpeed * Time.deltaTime;

            Vector3 translation = (xDirection * input.x + Vector3.up * input.y) * scaledSpeed;
            transform.position += translation;
        }

        private void WalkMove(Vector3 input)
        {
            if (CurrentState != State.walk) throw new Exception("The Unit cant walk right now!");

            float scaledMoveSpeed = _walkSpeed * Time.deltaTime;

            Vector3 move = new Vector3(input.x, 0f, input.y) * scaledMoveSpeed;
            transform.Translate(move);
        }
    }
}