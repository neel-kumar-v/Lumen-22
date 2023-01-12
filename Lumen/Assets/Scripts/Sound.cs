using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 [System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0, 3f)]
    public float volume = 0.7f;
    [Range(-3f, 3f)]
    public float pitch = 1f;

    public int priority;

    public bool loop;


    private AudioSource source;
    
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.priority = priority;
        source.loop = loop;
        source.Play();
    }
    
    public void Stop()
    {
        source.Stop();
    }
}
