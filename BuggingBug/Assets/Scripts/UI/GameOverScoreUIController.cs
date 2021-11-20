using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScoreUIController : MonoBehaviour
{
    [SerializeField]
    Text score;
    [SerializeField]
    Text best;
    [SerializeField]
    Text middle;
    [SerializeField]
    Text prevHighScore;
    [SerializeField]
    GameObject highScoreBanner;

    bool newHighScore;

    private void Start()
    {
        Events.events.OnNewHighScore += NewHighScore;
    }

    private void NewHighScore(float score, bool isNewHighScore)
    {
        if(isNewHighScore)
        {
            HighScoreUI(score);
        }
        else
        {
            StandardUI(score);
        }
    }

    private void StandardUI(float highscore)
    {
        best.text = "Best:";
        best.alignment = TextAnchor.MiddleRight;
        middle.enabled = false;
        prevHighScore.text = highscore.ToString("F0");
    }

    private void HighScoreUI(float highscore)
    {
        best.enabled = false;
        middle.enabled = false;
        prevHighScore.enabled = false;
        highScoreBanner.SetActive(true);
        score.text = highscore.ToString("F0");
        score.gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, 0.5f).SetEase(Ease.OutElastic);
    }
}
