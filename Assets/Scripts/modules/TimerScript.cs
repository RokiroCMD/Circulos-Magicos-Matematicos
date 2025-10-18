using System;
using System.Collections;
using UnityEngine;
using static GameManagerScript;
using UnityEngine.Events;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance;
    [SerializeField] public TextMeshProUGUI text;

    public int maxTime = 30;
    public int time = 0;
    Boolean active = false;

    public UnityEvent<int> TimeChangeEvent = new UnityEvent<int>();

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            text.text = maxTime + ":00";

        }

    }

    private void Start()
    {
        GameManagerScript.Instance.StateChangedEvent.AddListener(OnStateChanged);
    }


    public void updateTimerText()
    {
        int timeLeft = maxTime - time;
        text.text = timeLeft + ":00";
    }

    public void OnStateChanged(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.PAUSE:
                if (active)
                {
                    stopTimer();
                }
                break;
            case GameState.PLAYING:
                if (!active)
                {
                    startTimer();
                }
                break;
            case GameState.ENDING:
                if (active)
                {
                    stopTimer();
                }
                break;
        }
    }

    public void startTimer()
    {
        if (!active)
        {
            active = true;
            StartCoroutine(incrementTime());
        }
    }

    public void addTime(int plus)
    {
        maxTime += plus;
        //time -= plus;
    }

    IEnumerator incrementTime()
    {
        while (active)
        {
            yield return new WaitForSeconds(1);
            time += 1;
            TimeChangeEvent.Invoke(time);
        }
    }

    public void stopTimer()
    {
        active = false;
    }
}
