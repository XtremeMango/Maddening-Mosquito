using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionsMenuController : MonoBehaviour
{
    public void Open()
    {
        GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(Ease.OutBack);
    }

    public void Close()
    {
        GetComponent<RectTransform>().DOAnchorPosY(-590, 0.5f).SetEase(Ease.InBack);
    }
}
