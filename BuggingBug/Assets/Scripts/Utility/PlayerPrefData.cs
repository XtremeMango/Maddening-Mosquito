using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPrefData
{
    public float musicVol { get; set; }

    public PlayerPrefData(float musicVol)
    {
        this.musicVol = musicVol;
    }

    public PlayerPrefData(PlayerPrefData data)
    {
        this.musicVol = data.musicVol;
    }

}
