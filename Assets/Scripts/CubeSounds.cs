using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent (typeof (AudioSource))]
    public class CubeSounds : MonoBehaviour
    {

        [SerializeField] private AudioClip _clipEnter;

        private AudioSource _source;
        private const string Player = "Player";

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(nameof(Player))) return;

            _source.clip = _clipEnter;
            _source.Play();
        }
    }
}