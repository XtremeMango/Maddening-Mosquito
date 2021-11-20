using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Events.events.OnManClap += Shake;
    }

    private void Shake()
    {
        Camera.main.DOShakePosition(0.25f,2f);
    }
}
