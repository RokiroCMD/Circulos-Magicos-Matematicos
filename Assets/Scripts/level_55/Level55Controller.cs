using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Level55Controller : MonoBehaviour
{
    
    
    [SerializeField] public AppleController appleController;
    private GameManagerScript gameManagerScript;
    private ScoreScript scoreScript;
    private StarHUDScript starHUDScript;
    private PauseScript pauseScript;
    private TimerScript timerScript;
    [SerializeField] FinishScript finishScript;
    [SerializeField] StartScreenScript startScreenScript;

    private void Start()
    {
        gameManagerScript = GameManagerScript.Instance;
        scoreScript = ScoreScript.Instance;
        starHUDScript = StarHUDScript.Instance;
        pauseScript = PauseScript.instance;
        timerScript = TimerScript.instance;
        scoreScript.ScoreChangeEvent.AddListener(onScoreChange);
        starHUDScript.StarChangeEvent.AddListener(onStarChanged);
        gameManagerScript.StateChangedEvent.AddListener(onStateChanged);
        pauseScript.OnPauseEvent.AddListener(onPause);
        timerScript.TimeChangeEvent.AddListener(onTimeChange);
        gameManagerScript.changeState(GameManagerScript.GameState.STARTING);
        startScreenScript.ShowStartScreen();
    }

    public void onTimeChange(int i)
    {
        if (i >= timerScript.maxTime)
        {
            DOTween.CompleteAll();
            gameManagerScript.changeState(GameManagerScript.GameState.ENDING);
            timerScript.stopTimer();
        }

        if (i == 5)
        {
            CricketEventApple.instance.spawnCricket();
        }
        else if (i == 35)
        {
            CricketEventApple.instance.spawnCricket();
        }
        else if (i == 55)
        {
            CricketEventApple.instance.spawnCricket();
        }

    }

    public void onPause(bool isPaused)
    {

    }
    public void onStarChanged(int star)
    {

    }

    public void onScoreChange(int score)
    {

    }

    private void onStateChanged(GameManagerScript.GameState state)
    {
        switch (state)
        {
            case GameManagerScript.GameState.STARTING:
                break;
            case GameManagerScript.GameState.PLAYING:
                if (gameManagerScript.lastState == GameManagerScript.GameState.STARTING)
                {
                    startScreenScript.HideStartScreen();
                    AppleController.Instance.FillAppleValues();
                    AppleController.Instance.GenerateApples();

                }
                if (gameManagerScript.lastState == GameManagerScript.GameState.PAUSE)
                {
                    pauseScript.hidePauseScreen();
                }
                break;
            case GameManagerScript.GameState.PAUSE:
                pauseScript.showPauseScreen();
                break;
            case GameManagerScript.GameState.ENDING:
                
                break;
        }
    }

    public void resumeGame()
    {
        gameManagerScript.changeState(GameManagerScript.GameState.PLAYING);
    }

    public void pauseGame()
    {
        gameManagerScript.changeState(GameManagerScript.GameState.PAUSE);
    }
    
    
    public void RestartGame()
    {
        gameManagerScript.RestartScene();
    }

    public void startGame()
    {
        gameManagerScript.changeState(GameManagerScript.GameState.PLAYING);
    }

}
