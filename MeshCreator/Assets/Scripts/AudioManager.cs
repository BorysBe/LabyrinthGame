using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixer _audioMixer;
    public Sound[] _sounds;

    public static AudioManager instance;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in _sounds)
        {
            s._source = gameObject.AddComponent<AudioSource>();
            s._source.clip = s.clip;

            s._source.volume = s._volume;
            s._source.pitch = s._pitch;
            s._source.loop = s.loop;
            s._source.outputAudioMixerGroup = s._group;
        }
    }

    public void SetVolume(float globalVolume)
    {
        //float savedVolume = PlayerPrefs.SetFloat(0);

        _audioMixer.SetFloat("volume", globalVolume);
        //PlayerPrefs.SetFloat("volume", globalVolume);
        /*TODO
         * po zmianie sceny i ponownym erj�ciu do ustawie� d�wi�ku suwak resetuje swoje po�o�enie nie zmienia ju� g�o�no�ci
         */
    }



    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(_sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s._source.Play();
    }
}
