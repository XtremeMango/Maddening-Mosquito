using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    [SerializeField]
    public SoundClip[] uiSounds;

    private void Awake()
    {
        for (int i = 0; i < uiSounds.Length; i++)
        {
            uiSounds[i].source = gameObject.AddComponent<AudioSource>();
            uiSounds[i].source.clip = uiSounds[i].clip;
            uiSounds[i].source.volume = uiSounds[i].volume;
            uiSounds[i].source.pitch = uiSounds[i].pitch;
        }
    }

    public void PlayClip(string name)
    {
        SoundClip s = Array.Find(uiSounds, x => x.name == name);
        s.source.Play();
    }
}
