using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class PushButton : MonoBehaviour, IInteractable
    {
        [SerializeField] private float duration = 6.0f;

        public UnityEvent OnPush;
        public UnityEvent OnUnPush;

        private Animator _animator;
        private bool isPushed = false;
        private float unPushTime;
        private const string Push = "Push";
        private const string UnPush = "UnPush";

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Interact()
        {
            if (isPushed) return; 

            _animator.SetTrigger(nameof(Push));
            isPushed = true;
            unPushTime = Time.time + duration;
            OnPush?.Invoke();
        }

        public void Update()
        {
            if (!isPushed) return;

            if (Time.time >= unPushTime)
            {
                _animator.SetTrigger(nameof(UnPush));
                isPushed = false;

                OnUnPush?.Invoke();
            }
        }
    }
}
