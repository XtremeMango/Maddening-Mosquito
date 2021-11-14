using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    float scoreMultiplierRampRate = 0.2f;
    [SerializeField]
    float scoreMultiplierDecayRate = 0.3f;

    float currentScore;
    float currentPoints;
    float highScore = 800f;
    float scoreMultiplier = 1f;

    bool ramp;
    bool decay;

    private void Start()
    {
        Events.events.OnPointsCaptured += AddPoints;
        Events.events.OnMounted += Mounted;
        Events.events.OnDismount += Dismounted;
        Events.events.OnGameStateChangedToGameOver += CheckHighScore;
        highScore = GetComponent<SaveSystem>().LoadHighScore();
    }

    private void Dismounted()
    {
        AddPointsToScore();
        StartMultiplierDecay();
    }

    private void Mounted(ManSection obj)
    {
        StartMultiplierRampUp();
    }

    public Vector3 GetCurrentScoreInfo()
    {
        return new Vector3(currentPoints, currentScore, scoreMultiplier);
    }

    public void AddPoints(float points, float lvl)
    {
        currentPoints += Mathf.Ceil(points);
        UIEvents.uiEvents.UpdatePointsUI(currentPoints);
    }

    public void CheckHighScore()
    {
        if(currentScore > highScore)
        {
            highScore = currentScore;
            Events.events.NewHighScore(highScore,true);
            GetComponent<SaveSystem>().SaveHighScore(highScore);
        }
        else
        {
            Events.events.NewHighScore(highScore,false);
        }
    }

    public void AddPointsToScore()
    {
        currentScore += Mathf.Ceil(currentPoints * scoreMultiplier);
        currentPoints = 0;
        UIEvents.uiEvents.UpdatePointsUI(currentPoints);
        UIEvents.uiEvents.UpdateScoreUI(currentScore);
    }

    public void IncreaseScoreMultiplier(float amt,bool mult)
    {
        if (mult)
        {
            scoreMultiplier *= amt;
        }
        else
        {
            scoreMultiplier += amt;
        }
    }

    public void StartMultiplierRampUp()
    {
        ramp = true;
        decay = false;
        StartCoroutine("MultiplierRamp");
    }

    public void StartMultiplierDecay()
    {
        ramp = false;
        decay = true;
        StartCoroutine("MultiplierDecay");
    }

    public IEnumerator MultiplierRamp()
    {
        while (ramp)
        {
            IncreaseScoreMultiplier(scoreMultiplierRampRate * Time.deltaTime,false);
            UIEvents.uiEvents.UpdateMultUI(scoreMultiplier);
            yield return null;
        }
    }

    public IEnumerator MultiplierDecay()
    {
        while(decay)
        {
            scoreMultiplier = Mathf.Clamp(scoreMultiplier - scoreMultiplierDecayRate * Time.deltaTime,1,float.MaxValue);
            UIEvents.uiEvents.UpdateMultUI(scoreMultiplier);
            yield return null;
        }
    }

}
