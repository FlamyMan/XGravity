using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(PlayerMovement))]
    public class WalkSoundControl : MonoBehaviour
    {
        [SerializeField] private AudioSource _source;
        
        private PlayerMovement movement;

        private void Start()
        {
            movement = GetComponent<PlayerMovement>();
            movement.WalkStarted += OnWalkStarted;
            movement.WalkEnded += OnWalkEnded;
        }

        private void OnWalkStarted()
        {
            _source.UnPause();
        }

        private void OnWalkEnded()
        {
            _source.Pause();
        }
    }
}