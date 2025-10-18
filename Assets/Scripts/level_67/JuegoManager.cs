
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManagerScript;


public class JuegoManager : MonoBehaviour
{
    
    GameManagerScript gameManagerScript;
    TimerScript timerScript;
    StartScreenScript startScreenScript;

    [SerializeField] CangrejoManager cangrejoManager;
    [SerializeField] PelotaManager pelotaManager;
    
    int tiempoTranscurrido = 0;
    private void Awake()
    {
            
            
    }
    private void Start()
    {
        gameManagerScript = GameManagerScript.Instance;
        timerScript = TimerScript.instance;
        startScreenScript = GetComponent<StartScreenScript>();
        Debug.Log(gameManagerScript.getState());
        gameManagerScript.StateChangedEvent.AddListener(onGameStateChanged);
        startScreenScript.ShowStartScreen();
        timerScript.TimeChangeEvent.AddListener(onTimeChanged);
        PauseScript.instance.OnPauseEvent.AddListener(onPause);
        
    }

    void onPause(bool pause)
    {
        if (pause)
        {
            timerScript.stopTimer();
        }
        else
        {
            timerScript.startTimer();
        }
    }

    private void onTimeChanged(int time)
    {
        tiempoTranscurrido ++;
        llamarPelota(tiempoTranscurrido);
        timerScript.text.SetText( timerScript.maxTime - time+":00");
        if (timerScript.maxTime - time <= 0)
        {
            timerScript.stopTimer();
            gameManagerScript.changeState(GameState.ENDING);
        }
    }

    private void onGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameManagerScript.GameState.STARTING:
                break;
            case GameManagerScript.GameState.PLAYING:
                if (gameManagerScript.lastState == GameManagerScript.GameState.STARTING)
                {

                    comenzarJuego();
                }
                break;
            case GameManagerScript.GameState.PAUSE:
                break;
            case GameManagerScript.GameState.ENDING:
                break;
        }
    }
    void llamarPelota(int Tiempo){

        if (Tiempo % 15 == 0) {
                    Debug.Log("Lanzando pelota");
            int r = UnityEngine.Random.Range(0,2);
            if (r == 0) pelotaManager.crearPelota(PelotaManager.SalidaPelota.Izquierda);
            else pelotaManager.crearPelota(PelotaManager.SalidaPelota.Derecha);
        }

    }
    


    public void reiniciarJuego()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual);
    }

    public void comenzarJuego()
    {
        cangrejoManager.LlamarCangrejos();
    }


}
