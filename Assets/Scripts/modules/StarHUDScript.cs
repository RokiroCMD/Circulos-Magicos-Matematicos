using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StarHUDScript : MonoBehaviour
{
    public static StarHUDScript Instance;
    public UnityEvent<int> StarChangeEvent = new UnityEvent<int>();


    public int stars = 0;

    [SerializeField] GameObject Star1;
    [SerializeField] GameObject Star2;
    [SerializeField] GameObject Star3;
    [SerializeField] GameObject Star4;
    [SerializeField] GameObject Star5;

    [SerializeField] List<int> requiredStars;


    [SerializeField] Sprite StarOnSprite;
    [SerializeField] Sprite StarOffSprite;



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;

            Star1.GetComponent<Image>().sprite = StarOffSprite;
            Star2.GetComponent<Image>().sprite = StarOffSprite;
            Star3.GetComponent<Image>().sprite = StarOffSprite;
            Star4.GetComponent<Image>().sprite = StarOffSprite;
            Star5.GetComponent<Image>().sprite = StarOffSprite;


        }    
    }

    private void Start()
    {
        ScoreScript.Instance.ScoreChangeEvent.AddListener(onScoreChange);
    }

    private void onScoreChange(int score)
    {
        if (score >= requiredStars[0])
        {
            if (stars < 1)
            {
                addStar();
            }
        } 
        if (score >= requiredStars[1])
        {
            if (stars < 2)
            {
                addStar();
            }
        }
        if (score >= requiredStars[2])
        {
            if (stars < 3)
            {
                addStar();
            }
        } 
        if (score >= requiredStars[3])
        {
            if (stars < 4)
            {
                addStar();
            }
        } 
        if (score >= requiredStars[4])
        {
            if (stars < 5)
            {
                addStar();
            }
        }
    }

    public void addStar()
    {
        if (stars < 5) 
        {
            stars++;
            StarObtained();
            StarChangeEvent.Invoke(stars);
        }
    }

    void StarObtained()
    {
        switch (stars)
        {
            case 1:
                animateStarAppear(Star1);
                break;
            case 2:
                animateStarAppear(Star2);
                break;
            case 3:
                animateStarAppear(Star3);
                break;
            case 4:
                animateStarAppear(Star4);
                break;
            case 5:
                animateStarAppear(Star5);
                break;
        }
    }

    void animateStarAppear(GameObject starObject)
    {
        float tempY = starObject.transform.localPosition.y;
        float tempScale = starObject.transform.localScale.y * 1.25f;
        starObject.GetComponent<Image>().sprite = StarOnSprite;
        starObject.transform.DOPunchScale(new Vector3(tempScale, tempScale, 1f), 0.5f);

        // MOVIMIENTO HACIA ARRIBA
        starObject.transform.DOLocalMove(new Vector3( starObject.transform.localPosition.x, 0, starObject.transform.localPosition.z), 0.3f).SetEase(Ease.OutBounce)
            .OnComplete(()=> 
            {
                
                starObject.transform.DOLocalMove(new Vector3(starObject.transform.localPosition.x, tempY, starObject.transform.localPosition.z), 0.4f).SetEase(Ease.OutBounce)
            .OnComplete(() =>
            {
            });
            });
    }



}
