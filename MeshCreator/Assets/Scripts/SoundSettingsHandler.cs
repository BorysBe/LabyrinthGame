using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettingsHandler : MonoBehaviour
{

    public Slider SoundSlider;
    [SerializeField] float soundPreferences = 0;
    public AudioMixer _audioMixer;


    void Start()
    {
        bool result =_audioMixer.GetFloat("volume", out soundPreferences);

        if(result == true)
        {
            SoundSlider.value = soundPreferences;
        }
        else
        {
            SoundSlider.value = 0;
        }
    }


    public void SetVolume(float globalVolume)
    {
        _audioMixer.SetFloat("volume", globalVolume);
    }
}
