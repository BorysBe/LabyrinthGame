using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsHandler : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string SoundPref = "SoundPref";
    private int firstPlayInt;
    public Slider SoundSlider;
    private float soundFloat;


    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);

        if(firstPlayInt == 0)
        {
            soundFloat = .25f;
            SoundSlider.value = soundFloat;
            PlayerPrefs.SetFloat(SoundPref, soundFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            soundFloat = PlayerPrefs.GetFloat(SoundPref);
            SoundSlider.value = soundFloat;
        }
    }

    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat(SoundPref, SoundSlider.value);
    }

    private void OnApplicationFocus(bool inFocus)
    {
         if(!inFocus)
        {
            SaveSoundSettings();
        }
    }
}
