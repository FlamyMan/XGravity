using UnityEngine;
using UnityEngine.Events;

namespace Assets.code
{
    public class PressureButton : MonoBehaviour
    {
        [SerializeField] private Transform visual;

        private float _yOffset = 0.07f;
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
                visual.Translate(new(0, -_yOffset, 0), Space.World);
                OnActivation?.Invoke();
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            _collisionsCount--;
            if (_collisionsCount == 0 && _pressed)
            {
                _pressed = false;
                visual.Translate(new(0, _yOffset, 0), Space.World);
                OnDeactivation?.Invoke();
            }
        }
    }
}
