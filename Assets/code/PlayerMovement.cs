using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.code
{
    [RequireComponent (typeof(Rigidbody))]
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

        private Rigidbody rb;
        private bool _canBeRotated = true;
        private Vector3 _normal;
        private State _currentState = State.walk;
        

        public State CurrentState { 
            get { return _currentState; } 
            private set { ChangeState(value); } 
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Vector3 point = collision.GetContact(0).point;
            if (!collision.gameObject.CompareTag(nameof(Climbable))) return;
            _normal = collision.GetContact(0).normal;
            CurrentState = State.climb;

            transform.rotation.SetFromToRotation(Vector3.zero, -_normal);
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.CompareTag(nameof(Climbable))) return;
            CurrentState = State.walk;
        }

        private void ChangeState(State newState)
        {
            switch (newState)
            {
                case State.walk:
                    _canBeRotated = true;
                    rb.useGravity = true;
                    _currentState = State.walk;
                    break;
                case State.climb:
                    _canBeRotated = false;
                    rb.useGravity = false;
                    rb.velocity = Vector3.zero;
                    _currentState = State.climb;
                    break;
                default:
                    break;
            }
        }

        public bool TryRotate(Vector3 eulers)
        {
            if (!_canBeRotated) return false;

            transform.Rotate(eulers);
            return true;
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
                rb.AddForce(new(0, _jumpForce, 0), ForceMode.Impulse);
            }
        }

        private void WallJump()
        {
            rb.AddForce(_normal * _jumpForce, ForceMode.Impulse);
        }

        private bool IsOnGround()
        {
            if (rb.velocity.y == 0)
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
            if(CurrentState != State.climb) throw new Exception("The Unit cant climb right now!");

            Vector3 movement = _climbSpeed * Time.deltaTime * new Vector3(input.x, input.y, 0f);
            transform.Translate(movement);
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