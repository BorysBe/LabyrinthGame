using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    public AudioMixerGroup _group;

    [Range(0f, 1f)]
    public float _volume;
    [Range(.1f, 3f)]
    public float _pitch;

    public bool loop;
    
    [HideInInspector]
    public AudioSource _source;
}
