using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Events : MonoBehaviour
{
    public static Events events;
    private void Awake()
    {
        events = this;
    }

    public event Action OnLeftMBClicked;
    public void LeftMBClicked()
    {
        OnLeftMBClicked?.Invoke();
    }

    public event Action OnRightMBClicked;
    public void RightMBClicked()
    {
        OnRightMBClicked?.Invoke();
    }

    public event Action OnGameStateChangedToWaitingStart;
    public void GameStateChangedToWaitingStart()
    {
        OnGameStateChangedToWaitingStart?.Invoke();
    }
    
    public event Action OnGameStateChangedToActive;
    public void GameStateChangedToActive()
    {
        OnGameStateChangedToActive?.Invoke();
    }

    public event Action OnGameStateChangedToGameOver;
    public void GameStateChangedToGameOver()
    {
        OnGameStateChangedToGameOver?.Invoke();
    }

    public event Action<ManSection> OnMounted;
    public void Mounted(ManSection section)
    {
        OnMounted?.Invoke(section);
    }

    public event Action<float,float> OnPointsCaptured;
    public void PointsCaptured(float capturedPoints, float pointLevel)
    {
        OnPointsCaptured?.Invoke(capturedPoints, pointLevel);
    }

    public event Action<float> OnBloodRemovedFromSection;
    public void BloodRemovedFromSection(float amt)
    {
        OnBloodRemovedFromSection?.Invoke(amt);
    }


    public event Action OnDismount;
    public void Dismount()
    {
        OnDismount?.Invoke();
    }


    public event Action OnSuckAttempted;
    public void SuckAttempted()
    {
        OnSuckAttempted?.Invoke();
    }

    public event Action OnManAttack;
    public void ManAttack()
    {
        OnManAttack?.Invoke();
    }

    public event Action OnAttackComplete;
    public void AttackComplete()
    {
        OnAttackComplete?.Invoke();
    }

    public event Action OnPlayerHit;
    public void PlayerHit()
    {
        OnPlayerHit?.Invoke();
    }

    public event Action OnPlayerOutOfBlood;
    public void PlayerOutOfBlood()
    {
        OnPlayerOutOfBlood?.Invoke();
    }

    public event Action<float,bool> OnNewHighScore;
    public void NewHighScore(float score,bool isNewHighScore)
    {
        OnNewHighScore?.Invoke(score,isNewHighScore);
    }

    public event Action OnManClap;

    public void ManClap()
    {
        OnManClap?.Invoke();
    }



}
