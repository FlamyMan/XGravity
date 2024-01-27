namespace Assets.Scripts
{
    [System.Serializable]
    public class PlayerLoadingPref
    {
        public AudioSavings AudioSaves;
    }
    [System.Serializable]
    public class AudioSavings
    {
        public string[] Mixer;
        public string[] SavedName;
    }
}
