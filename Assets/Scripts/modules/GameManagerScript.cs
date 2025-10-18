using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript Instance;

    public UnityEvent<GameState> StateChangedEvent = new UnityEvent<GameState>();
    public GameState lastState;
    private GameState gameState;

    public void RestartScene()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual);
    }

    private void Awake()
    {
        gameState = GameState.STARTING;
        lastState = gameState;
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    public enum GameState
    {
        STARTING,
        PLAYING,
        PAUSE,
        ENDING
    }

    public GameState getState() { return gameState; }

    public void changeState(GameState state)
    {
        lastState = gameState;
        gameState = state;
        StateChangedEvent.Invoke(state);
    }

    public void changeState(int state)
    {
        lastState = gameState;
        switch (state)
        {
            case 0:
                gameState = GameState.STARTING;
                break;
            case 1:
                gameState = GameState.PLAYING;
                break;
            case 2:
                gameState = GameState.PAUSE;
                break;
            case 3:
                gameState = GameState.ENDING;
                break;
        }
        StateChangedEvent.Invoke(gameState);
    }

}
