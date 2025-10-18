using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance;

    public UnityEvent<int> ScoreChangeEvent = new UnityEvent<int>();

    public int score = 0;

    [SerializeField] TextMeshProUGUI scoreTXT;

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

    public void addScore(int add)
    {
        score += add;
        changeScoreTXT();
        ScoreChangeEvent.Invoke(score);
    }

    public void setScore(int set)
    {
        score = set;
        changeScoreTXT();
        ScoreChangeEvent.Invoke(score);
    }

    public void removeScore(int remove)
    {
        score -= remove;
        changeScoreTXT();
        ScoreChangeEvent.Invoke(score);
    }

    void changeScoreTXT()
    {
        scoreTXT.text = ""+score;
    }

}
