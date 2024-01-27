using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class PressureButton : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private bool _pressed = false;
        private int _collisionsCount;

        public UnityEvent OnActivation;
        public UnityEvent OnDeactivation;

        private void OnCollisionEnter(Collision collision)
        {
            _collisionsCount++;
            if (_collisionsCount > 0 && !_pressed)
            {
                _pressed = true;
                _animator.SetTrigger("trPress");
                OnActivation?.Invoke();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            _collisionsCount--;
            if (_collisionsCount == 0 && _pressed)
            {
                _pressed = false;
                _animator.SetTrigger("trUnpress");
                OnDeactivation?.Invoke();
            }
        }
    }
}
