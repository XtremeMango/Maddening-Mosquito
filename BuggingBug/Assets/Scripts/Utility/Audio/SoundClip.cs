using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundClip
{
    public string name;

    public AudioClip clip;
    [Range(0f,2f)]
    public float volume = 1f;
    [Range(0.1f,3f)]
    public float pitch = 1f;

    [HideInInspector]
    public AudioSource source;
}

