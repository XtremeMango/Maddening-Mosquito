using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefData
{
    float musicVol;
    float sfxVol;

    public PlayerPrefData(float musicVol, float sfxVol)
    {
        this.musicVol = musicVol;
        this.sfxVol = sfxVol;
    }

    public PlayerPrefData(PlayerPrefData data)
    {
        this.musicVol = data.musicVol;
        this.sfxVol = data.sfxVol;
    }
}
