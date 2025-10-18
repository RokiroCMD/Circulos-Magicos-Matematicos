using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManagerScript;

public class Level22Controller : MonoBehaviour
{

    public static Level22Controller Instance;

    [SerializeField] private AudioSource music;

    private TimerScript timerScript;
    private PauseScript pauseScript;
    private GameManagerScript gameManager;
    private StarHUDScript starHUDScript;
    public ScoreScript scoreScript;
    private BeeManager beeManager;
    private ValuesGeneratorScripts valuesGenerator;
    private SoundManager soundManager;

    [SerializeField] private StartScreenScript startScreenScript;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        gameManager = GameManagerScript.Instance;
        scoreScript = ScoreScript.Instance;
        pauseScript = PauseScript.instance;
        timerScript = TimerScript.instance;
        starHUDScript =  StarHUDScript.Instance;
        gameManager.StateChangedEvent.AddListener(onGameStateChanged);
        pauseScript.OnPauseEvent.AddListener(onPause);
        timerScript.TimeChangeEvent.AddListener(onTimeChanged);
        beeManager = BeeManager.instance;
        valuesGenerator = ValuesGeneratorScripts.Instance; 
        soundManager = SoundManager.instance;
        startScreenScript.ShowStartScreen();
    }

    public void StartGame()
    {
        startScreenScript.HideStartScreen();
        PlayerPrefs.SetInt("lvl" + 22 + "_record", 0);

    }

    private void onGameStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.STARTING:
                
                break;
            case GameState.PLAYING:
                break;
            case GameState.PAUSE:
                break;
            case GameState.ENDING:
                beeManager.disableAllBees();
                music.Stop();
                break;
        }
    }

    private void onTimeChanged(int time)
    {
        
        if (time >= timerScript.maxTime)
        {
            gameManager.changeState(GameState.ENDING);
        }
        if (time == 1)
        {
            beeManager.addBee();
        }
        else if (time == 2)
        {
            beeManager.addBee();
            CricketEvent.instance.spawnCricket();
        }
        else if (time == 4)
        {
            beeManager.addBee();
        }
        else if (time == 5)
        {
            beeManager.addBee();
        }
        else if (time == 6)
        {
            beeManager.addBee();
        }
        else if (time == 15)
        {
            CricketEvent.instance.spawnCricket();
        }
        else if (time == 20)
        {
            BeeManager.instance.EXTRA = true;
            
        }
        else if (time == 35)
        {
            BeeManager.instance.EXTRA = true;
            CricketEvent.instance.spawnCricket();
        }
        else if (time == 45)
        {
            BeeManager.instance.EXTRA = true;
        }
        else if (time == 50)
        {
            CricketEvent.instance.spawnCricket();
        }
        else if (time == 55)
        {
            BeeManager.instance.EXTRA = true;
        }
    }

    private void onPause(bool status)
    {
        switch(status)
        {
            case true:
                gameManager.changeState(GameState.PAUSE);
                beeManager.pauseBees();
                break; 
            case false:
                gameManager.changeState(GameState.PLAYING);
                beeManager.resumeBees();
                break;
        }
    }

}
