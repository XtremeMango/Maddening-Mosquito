using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class ScoreUIController : MonoBehaviour
{
    [SerializeField]
    Text score;
    [SerializeField]
    Text points;
    [SerializeField]
    Text mult;
    [SerializeField]
    Vector2 punchDist;
    [SerializeField]
    float punchDur;

    bool hasControl;

    private void Start()
    {
        hasControl = true;
        UIEvents.uiEvents.OnUpdateScoreUI += UpdateScore;
        UIEvents.uiEvents.OnUpdatePointsUI += UpdatePoints;
        UIEvents.uiEvents.OnUpdateMultUI += UpdateMult;
        Events.events.OnGameStateChangedToGameOver += Disable;
        mult.text = "1.0";
        points.text = "0";
        score.text = "0";
    }

    private void Disable()
    {
        hasControl = false;
    }

    private void UpdateMult(float val)
    {
        if (hasControl)
        {
            mult.text = val.ToString("F1");
        }
    }

    private void UpdatePoints(float val)
    {
        if (hasControl)
        {
            points.text = val.ToString("F0");
            DoPunch(points.gameObject.GetComponent<RectTransform>());
        }
    }

    private void UpdateScore(float val)
    {
        if (hasControl)
        {
            score.text = val.ToString("F0");
            DoPunch(score.gameObject.GetComponent<RectTransform>());
        }
    }

    public void DoPunch(RectTransform rect)
    {
        rect.DOPunchAnchorPos(punchDist, punchDur,5,0.2f);
    }
}
