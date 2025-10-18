using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManagerScript;

public class FinishScript : MonoBehaviour
{

    [SerializeField] private GameObject finishScreen;
    [SerializeField] private GameObject finishPanel;


    [SerializeField] private GameObject ScoreStar01;
    [SerializeField] private GameObject ScoreStar02;
    [SerializeField] private GameObject ScoreStar03;
    [SerializeField] private GameObject ScoreStar04;
    [SerializeField] private GameObject ScoreStar05;

    [SerializeField] private TextMeshProUGUI recordTXT;
    [SerializeField] private TextMeshProUGUI newRecordTXT;
    [SerializeField] private Sprite StarOffSprite;
    [SerializeField] private Sprite StarOnSprite;

    [SerializeField] AudioClip finishSFX;

    [SerializeField] string levelName;
    public bool IsFinishScreenActive { get; private set; }

    GameManagerScript gameManagerScript;
    StarHUDScript starHUDScript;
    ScoreScript scoreScript;

    private void Start()
    {
        gameManagerScript = GameManagerScript.Instance;
        starHUDScript = StarHUDScript.Instance;
        scoreScript = ScoreScript.Instance; 
        gameManagerScript.StateChangedEvent.AddListener(OnStateChanged);
    }


    private void OnStateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.ENDING:
                showFinishScreen();
                break;
        }
    }

    public void calculateRecord()
    {
        int puntuacionGuardada = PlayerPrefs.GetInt("lvl"+levelName+"_record");
        if (puntuacionGuardada < scoreScript.score)
        {
            newRecordTXT.gameObject.SetActive(true);
            PlayerPrefs.SetInt("lvl"+levelName+"_record", scoreScript.score);
            recordTXT.text = "Record: " + scoreScript.score;
        }
        else
        {
            recordTXT.text = "Record: " + puntuacionGuardada;
        }
    }

    public void showFinishScreen()
    {
        calculateRecord();

        CanvasGroup cgFinish = finishScreen.GetComponent<CanvasGroup>();
        finishScreen.SetActive(true);
        finishPanel.transform.localPosition = new Vector3(0, 1500, 0);

        finishPanel.transform.localScale = new Vector3(0, 0, 0);

        finishPanel.transform.DOLocalMove(Vector3.zero, 0.8f).SetEase(Ease.OutSine);
        finishPanel.transform.DOScale(1, 0.8f).SetEase(Ease.OutBounce).OnComplete(() => {

            showStarAnimation();
            sequence.OnComplete(() =>
            {
                ScoreStar01.transform.parent.transform.DOPunchScale(new Vector3(0.3f,0.3f,0.3f), 1f);
                gameObject.GetComponent<AudioSource>().PlayOneShot(finishSFX);
            });
            cgFinish.interactable = true;
            cgFinish.blocksRaycasts = true; 
            IsFinishScreenActive = true;
        });
       
    }

    private Sequence sequence;
    void showStarAnimation()
    {
        sequence = DOTween.Sequence();
        if (starHUDScript.stars > 0)
        {
            animateStarAppear(ScoreStar01);
        }
        if (starHUDScript.stars > 1)
        {
            animateStarAppear(ScoreStar02);
        }
        if (starHUDScript.stars > 2)
        {
            animateStarAppear(ScoreStar03);
        }
        if (starHUDScript.stars > 3)
        {
            animateStarAppear(ScoreStar04);
        }
        if (starHUDScript.stars > 4)
        {
            animateStarAppear(ScoreStar05);
        }
    }

    void animateStarAppear(GameObject starObject)
    {

        float tempY = starObject.transform.localPosition.y;
        float tempScale = starObject.transform.localScale.y * 1.25f;
        
        

        sequence.Append(starObject.transform.DOLocalMove(new Vector3(starObject.transform.localPosition.x, 0, starObject.transform.localPosition.z), 0.2f).SetEase(Ease.OutBounce).OnStart(() => {
            starObject.GetComponent<Image>().sprite = StarOnSprite;
            starObject.GetComponent<AudioSource>().Play();
        }));
        sequence.Join(starObject.transform.DOPunchScale(new Vector3(tempScale, tempScale, 1f), 0.4f));
        sequence.Append(starObject.transform.DOLocalMove(new Vector3(starObject.transform.localPosition.x, tempY, starObject.transform.localPosition.z), 0.2f).SetEase(Ease.OutBounce));

    }



}
