using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerAudioController : MonoBehaviour
{
    
    [SerializeField] AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        Events.events.OnPlayerOutOfBlood += Whimper;
        Events.events.OnPlayerHit += Stop;
    }

    private void Stop()
    {
        source.Stop();
    }

    private void Whimper()
    {
        source.DOFade(0, 2f).SetEase(Ease.Linear);
        source.DOPitch(0.1f, 1f).SetEase(Ease.InBack);
    }

}
