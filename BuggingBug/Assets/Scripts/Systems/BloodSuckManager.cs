using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSuckManager : MonoBehaviour
{
    [SerializeField]
    float aimReticuleLoopFreq;
    [SerializeField]
    Vector2[] pointDecayMultiplier;

    bool SuckGameActive;
    float currentAimVal;
    bool getScore;

    ManSection currentSection;

    private void Start()
    {
        Events.events.OnMounted += StartSuckGame;
        Events.events.OnDismount += StopSuckGame;
        Events.events.OnRightMBClicked += GetScore;
        SuckGameActive = false;
        currentAimVal = 0;
    }

    private void StopSuckGame()
    {
        SuckGameActive = false;
        StopCoroutine("Suck");
    }

    private void GetScore()
    {
        Events.events.SuckAttempted();
        getScore = true;
    }

    private void StartSuckGame(ManSection section)
    {
        currentSection = section;
        SuckGameActive = true;
        currentSection.ToggleMounted();
        StartCoroutine("Suck");
    }

    public IEnumerator Suck()
    {
        while(SuckGameActive)
        {
            currentAimVal -= aimReticuleLoopFreq * 360f * Time.deltaTime;
            if(currentAimVal <= -360)
            {
                currentAimVal = currentAimVal + 360;
            }
            UIEvents.uiEvents.UpdateSuckUIReticulePos(currentAimVal);
            if(getScore && currentSection.HasBlood())
            {
                float points = 0;
                float pointMultLevel = 1;
                for (int i = 0; i < pointDecayMultiplier.Length; i++)
                {
                    if (Mathf.Abs(Mathf.DeltaAngle(180,currentAimVal)) <= pointDecayMultiplier[i].x)
                    {
                        Debug.Log(Mathf.Abs(Mathf.DeltaAngle(180, currentAimVal)));
                        Debug.Log(pointDecayMultiplier[i].x);
                        points = Mathf.Ceil(currentSection.data.pointValue * pointDecayMultiplier[i].y);
                        pointMultLevel = 3 - i;
                        break;
                    }
                }
                Events.events.PointsCaptured(points, pointMultLevel);
                getScore = false;
            }
            yield return null;
        }
    }
}
