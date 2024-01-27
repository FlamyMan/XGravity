using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class AudioControl : MonoBehaviour
{
    [SerializeField] private string _savingName;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _mixerParamName;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        if(PlayerPrefs.HasKey(_savingName))
        {
            LoadVolume();
        }
        else
        {
            ChangeVolume(_slider.value);
        }
    }

    public void ChangeVolume(float newValue)
    {
        _mixer.SetFloat(_mixerParamName, Mathf.Log10(newValue) * 20);
        PlayerPrefs.SetFloat(_savingName, newValue);
        
    }

    private void LoadVolume()
    {
        _slider.value = PlayerPrefs.GetFloat(_savingName);
        ChangeVolume(_slider.value);
    }
}
