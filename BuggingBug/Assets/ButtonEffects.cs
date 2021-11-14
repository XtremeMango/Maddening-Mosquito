using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float speed;
    public Vector3 scaleAmount;
    public Ease scaleEase;

    Vector3 baseScale;
    public void OnPointerEnter(PointerEventData eventData)
    {
        baseScale = transform.localScale;
        transform.DOScale(baseScale + scaleAmount, speed).SetEase(scaleEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(baseScale, speed).SetEase(scaleEase);
    }
}
