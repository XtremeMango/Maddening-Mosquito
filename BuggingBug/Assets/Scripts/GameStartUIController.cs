using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameStartUIController : MonoBehaviour
{
    public GameObject frontImage;

    private void Awake()
    {
        frontImage.SetActive(true);
    }

    private void WipeCover()
    {
        frontImage.GetComponent<RectTransform>().DOAnchorPosY(-1200, 1f).SetEase(Ease.InOutQuad).SetAutoKill();
    }

    // Start is called before the first frame update
    void Start()
    {
        Events.events.OnGameStateChangedToWaitingStart += WipeCover;
        Events.events.OnGameStateChangedToActive += DisableUI;
    }

    private void DisableUI()
    {
        gameObject.SetActive(false);
    }
    
    
}
