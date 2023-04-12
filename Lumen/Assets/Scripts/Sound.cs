using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup mixer;

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

    public void Play() {
        source.outputAudioMixerGroup = mixer;
        source.volume = volume;
        source.pitch = pitch;
        source.priority = priority;
        source.loop = loop;
        source.Play();
    }
    public void Pause() {
        source.Pause();
    }
    public void Unpause() {
        source.UnPause();
    }
    
    public void Stop()
    {
        source.Stop();
    }
    public bool IsPlaying()
    {
        return source.isPlaying;
    }
}
