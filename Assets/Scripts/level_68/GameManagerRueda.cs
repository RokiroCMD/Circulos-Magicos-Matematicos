using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManagerRueda : MonoBehaviour
{
    public static GameManagerRueda Instance;

    [SerializeField] private UIControllerRueda uiController;
    public GameState lastState;
    private GameState gameState;
    public int maxTime = 30;
    private int score = 0;
    private bool haveStar1 = false, haveStar2 = false, haveStar3 = false, haveStar4 = false, haveStar5 = false;


    public UnityEvent<GameState> StateChangedEvent = new UnityEvent<GameState>();
    public UnityEvent<int> StarObtainedEvent = new UnityEvent<int>();
    private void Awake()
    {
        lastState = gameState;
        gameState = GameState.STARTING;
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        changeState(GameState.STARTING);
    }

    public void onTimeChange(int time)
    {
        /*if (time > maxTime)
        {
            TimerController.instance.stopTimer();
            changeState(GameState.ENDING);
        }
        else
        {
            uiController.changeTimer(time);
        }*/
    }

    public void addScore(int points)
    {
        score += points;
        //uiController.setScore(score);
        if (score >= 3 && !haveStar1)
        {
            haveStar1 = true;
            StarObtainedEvent.Invoke(1);
        }
        if (score >= 6 && !haveStar2)
        {
            haveStar2 = true;
            StarObtainedEvent.Invoke(2);
        }
        if (score >= 8 && !haveStar3)
        {
            haveStar3 = true;
            StarObtainedEvent.Invoke(3);
        }
        if (score >= 10 && !haveStar4)
        {
            haveStar4 = true;
            StarObtainedEvent.Invoke(4);
        }
        if (score >= 14 && !haveStar5)
        {
            haveStar5 = true;
            StarObtainedEvent.Invoke(5);
        }

    }
    public int getScore() { return score; }

    public GameState getState() { return gameState; }

    public void changeState(GameState state)
    {
        lastState = gameState;
        gameState = state;
        StateChangedEvent.Invoke(state);
    }

    public void RestartGame()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual);
    }

    public void StartGame()
    {
        /*uiController.HideStartScreen();
        TimerController.instance.TimeChangeEvent.AddListener(onTimeChange);
        */
    }

    public enum GameState
    {
        STARTING,
        PLAYING,
        PAUSE,
        ENDING
    }
}
