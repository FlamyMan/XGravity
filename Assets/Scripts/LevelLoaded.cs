using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts
{
    public class LevelLoaded : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;
        private
        private void Start()
        {
            LoadAudioSettings();
        }

        public void LoadAudioSettings()
        {
            
        }
    }
}