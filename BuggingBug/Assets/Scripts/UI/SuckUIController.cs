using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SuckUIController : MonoBehaviour
{
    [SerializeField]
    GameObject SuckUI;
    [SerializeField]
    GameObject reticule;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject sectionBlood;

    RectTransform rect;
    float radius;
    private void Awake()
    {
        rect = reticule.GetComponent<RectTransform>();
        radius = rect.anchoredPosition.magnitude;
    }

    private void Start()
    {
        UIEvents.uiEvents.OnUpdateSuckUIReticulePos += UpdatePos;
        UIEvents.uiEvents.OnUpdateSectionBloodUI += ReduceBlood;
        Events.events.OnMounted += ShowSuckUI;
        Events.events.OnDismount += HideSuckUI;
        Events.events.OnGameStateChangedToGameOver += DisableSuckUI;
    }

    private void DisableSuckUI()
    {
        HideSuckUI();
        this.enabled = false;
    }

    private void ReduceBlood(float val)
    {
        sectionBlood.GetComponent<Image>().DOFillAmount(val, 0.5f).SetEase(Ease.OutQuad);
    }

    private void HideSuckUI()
    {
        SuckUI.SetActive(false);
    }

    private void ShowSuckUI(ManSection section)
    {
        GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        sectionBlood.GetComponent<Image>().fillAmount = section.GetBloodAmountRatio();
        SuckUI.SetActive(true);
    }

    private void UpdatePos(float deg)
    {
        if (SuckUI.activeSelf)
        {
            Vector2 newPos = radius * new Vector2(Mathf.Cos(Mathf.Deg2Rad * (deg-90)), Mathf.Sin(Mathf.Deg2Rad * (deg-90)));
            rect.anchoredPosition = newPos;
            rect.localRotation = Quaternion.Euler(0,0,deg);
        }
    }

}
