using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BloodFillUIController : MonoBehaviour
{
    [SerializeField]
    Image fill;
    [SerializeField]
    float fillSpeed;
    [SerializeField]
    Ease fillEase;
    [SerializeField]
    Image logo;
    [SerializeField]
    float lowPulseSize;
    [SerializeField]
    float lowPulseSpeed;
    [SerializeField]
    float lowThreshold;

    bool pulsing;
    Tweener imagePulse;

    private void Start()
    {
        UIEvents.uiEvents.OnUpdateBloodUI += UpdateFill;
    }

    private void UpdateFill(float val)
    {
        DoFillChange(val);
        if (val <= lowThreshold && !pulsing)
        {
            pulsing = true;
            StartImagePulse();
        }
        else if (val > lowThreshold && pulsing)
        {
            pulsing = false;
            StopImagePulse();
        }
    }

    private void DoFillChange(float val)
    {
        fill.DOFillAmount(val, fillSpeed).SetEase(fillEase);
    }

    private void StartImagePulse()
    {
        imagePulse = logo.transform.DOPunchScale(logo.transform.localScale * lowPulseSize, lowPulseSpeed,10,0.5f).SetLoops(-1);
    }

    private void StopImagePulse()
    {
        imagePulse.Kill();
    }
}
