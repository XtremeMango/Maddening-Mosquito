using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicController : MonoBehaviour
{
    public AudioSource main;

    SaveSystem save;

    private void Awake()
    {
        save = FindObjectOfType<SaveSystem>();
        PlayerPrefData data = save.LoadPlayerPrefs();
        main.volume = data.musicVol;
    }

    public void SetVol(float val)
    {
        main.volume = val;
    }

    public float GetVol()
    {
        return main.volume;
    }

    public void SaveVolume()
    {
        save.SavePlayerPrefs(new PlayerPrefData(main.volume));
    }
}
