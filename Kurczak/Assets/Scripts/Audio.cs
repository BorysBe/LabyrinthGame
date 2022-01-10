using System;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public Sound[] _sounds;


    public static Audio instance = null;


    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }



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
