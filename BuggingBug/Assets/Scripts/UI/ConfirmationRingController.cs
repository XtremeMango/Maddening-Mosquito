using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConfirmationRingController : MonoBehaviour
{
    public Color highColor;
    public Color midColor;
    public Color lowColor;

    public float highScale;
    public float highScaleSpeed;

    public float midScale;
    public float midScaleSpeed;

    public float lowScale;
    public float lowScaleSpeed;

    Image ring;
    RectTransform ringTrans;
    Tweener scaler;
    Tweener colerer;
    // Start is called before the first frame update
    void Start()
    {
        UIEvents.uiEvents.OnSuckGameAttempted += Play;
        ring = GetComponent<Image>();
        ringTrans = GetComponent<RectTransform>();
    }

    private void Play(int result)
    {
        switch (result)
        {
            case 3:
                DoEffect(highScaleSpeed, highColor, highScale);
                break;
            case 2:
                DoEffect(midScaleSpeed, midColor, midScale);
                break;
            case 1:
                DoEffect(lowScaleSpeed, lowColor, lowScale);
                break;
            default:
                break;
        }
    }

    public void DoEffect(float dur, Color col, float scale)
    {
        ring.color = col;
        ringTrans.localScale = Vector3.one;
        scaler.Kill();
        colerer.Kill();
        scaler =  ringTrans.DOScale(Vector3.one * scale, dur).SetEase(Ease.OutCubic);
        colerer = ring.DOColor(Color.clear, dur).SetEase(Ease.OutQuart);


    }
}
