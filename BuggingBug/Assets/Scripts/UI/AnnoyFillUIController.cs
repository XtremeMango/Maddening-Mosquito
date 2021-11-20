using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnnoyFillUIController : MonoBehaviour
{
    [SerializeField]
    float fillSpeed;
    [SerializeField]
    Ease fillEase;

    Image fill;
    private void Awake()
    {
        Image[] x = gameObject.GetComponentsInChildren<Image>();
        for (int i = 0; i < x.Length; i++)
        {
            if (x[i].name == "Fill")
            {
                fill = x[i];
                break;
            }
        }
    }

    private void Start()
    {
        UIEvents.uiEvents.OnUpdateAnnoyUI += UpdateFill;
    }

    private void UpdateFill(float val)
    {
        DoFillChange(val);
    }

    private void DoFillChange(float val)
    {
        fill.DOFillAmount(val, fillSpeed).SetEase(fillEase);
    }
}
