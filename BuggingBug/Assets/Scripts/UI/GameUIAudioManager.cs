using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameUIAudioManager : MonoBehaviour
{
    public SoundClip[] sounds;

    private void Awake()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i].source = gameObject.AddComponent<AudioSource>();
            sounds[i].source.clip = sounds[i].clip;
            sounds[i].source.volume = sounds[i].volume;
            sounds[i].source.pitch = sounds[i].pitch;
        }
    }

    private void Start()
    {
        UIEvents.uiEvents.OnPlayGameUISound += PlayClip;
    }

    public void PlayClip(string name)
    {
        if (name == "Grunt")
        {
            int num = UnityEngine.Random.Range(1, 3);
            name = "ManAnnoy_" + num.ToString();
        }
        SoundClip s = Array.Find(sounds, x => x.name == name);
        s.source.Play();
    }
    public void StopClip(string name)
    {
        if (name == "Grunt")
        {
            int num = UnityEngine.Random.Range(1, 7);
            name = "ManAnnoy_" + num.ToString();
        }
        SoundClip s = Array.Find(sounds, x => x.name == name);
        s.source.Stop();
    }
}
