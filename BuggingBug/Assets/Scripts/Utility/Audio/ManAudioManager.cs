using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManAudioManager : MonoBehaviour
{
    public SoundClip[] clips;

    private void Awake()
    {
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].source = gameObject.AddComponent<AudioSource>();
            clips[i].source.clip = clips[i].clip;
            clips[i].source.volume = clips[i].volume;
            clips[i].source.pitch = clips[i].pitch;
        }
    }

    private void Start()
    {
        Events.events.OnPlayManSound += PlayClip;
        Events.events.OnStopManSound += StopClip;
        Events.events.OnGameStateChangedToGameOver += PlaySigh;
    }

    private void PlaySigh()
    {
        PlayClip("ManRelief");
    }

    public void PlayClip(string name)
    {
        if (name == "Grunt")
        {
            int num = UnityEngine.Random.Range(1, 3);
            name = "ManAnnoy_" + num.ToString();
        }
        SoundClip s = Array.Find(clips, x => x.name == name);
        s.source.Play();
    }
    public void StopClip(string name)
    {
        if (name == "Grunt")
        {
            int num = UnityEngine.Random.Range(1, 7);
            name = "ManAnnoy_" + num.ToString();
        }
        SoundClip s = Array.Find(clips, x => x.name == name);
        s.source.Stop();
    }
}
