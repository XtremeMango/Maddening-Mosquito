using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class GameStateManager : MonoBehaviour
{
    enum GameState { Initilize,WaitingStart,Active,GameOver}
    GameState gameState;
    bool firstframe;

    private void Awake()
    {
        gameState = GameState.Initilize;
        DOTween.Init().SetCapacity(1250, 50);
        firstframe = true;
    }

    private void Start()
    {
        Events.events.OnLeftMBClicked += StartGame;
        Events.events.OnPlayerHit += GameOver;
        Events.events.OnPlayerOutOfBlood += GameOver;
    }

    public void StartGame()
    {
        if (gameState == GameState.WaitingStart)
        {
            gameState = GameState.Active;
            Events.events.GameStateChangedToActive();
        }
    }

    public void GameOver()
    {
        if (gameState == GameState.Active)
        {
            gameState = GameState.GameOver;
            Events.events.GameStateChangedToGameOver();
        }
    }

    public void RestartGame()
    {
        if (gameState == GameState.GameOver)
        {
            DOTween.KillAll();
            DOTween.Clear();
            SceneManager.LoadScene(1);
        }
    }

    public void BackToTitle()
    {
        if (gameState == GameState.GameOver)
        {
            DOTween.KillAll();
            DOTween.Clear();
            SceneManager.LoadScene(0);
        }
    }

    private void Update()
    {
        if (firstframe)
        {
            gameState = GameState.WaitingStart;
            Events.events.GameStateChangedToWaitingStart();
            firstframe = false;
        }
        UIEvents.uiEvents.UpdateDebugUIElement_PerFrame("FPS", 1f / Time.deltaTime);
    }

}
