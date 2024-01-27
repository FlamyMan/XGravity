using System.IO;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts
{
    public class LevelLoaded : MonoBehaviour
    {
        [SerializeField] private AudioMixer _mixer;

        private PlayerLoadingPref preference;
        private static string path = @"Assets/Jsons/PlayerSavings.json";

        private void Start()
        {
            string rawJson = File.ReadAllText(path);
            preference = JsonUtility.FromJson<PlayerLoadingPref>(rawJson);

            LoadAudioSettings();
        }

        public void LoadAudioSettings()
        {
            AudioSavings savings = preference.AudioSaves;
            for (int i = 0; i < savings.Mixer.Length; i++) 
            {
                float val = PlayerPrefs.GetFloat(savings.SavedName[i]);
                _mixer.SetFloat(savings.Mixer[i], Mathf.Log10(val) * 20);
            }
        }
    }
}