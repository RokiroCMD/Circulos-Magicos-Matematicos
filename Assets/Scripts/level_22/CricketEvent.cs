using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class CricketEvent : MonoBehaviour
{
    public static CricketEvent instance;
    Tween grillo; 
    float tempX; float tempY;


    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else { 
            Destroy(gameObject);
        }
    }

    public void spawnCricket()
    {
        SoundManager.instance.playOnce(SFXS.critsolo);
        tempX = transform.localPosition.x;
        tempY = transform.localPosition.y;
        grillo =  transform.DOLocalJump(new Vector3(Screen.currentResolution.width / 2 + 1000, transform.localPosition.y, 1), 700f, 4, 6f).SetDelay(3f).OnComplete(() =>
        {
            transform.localPosition = new Vector3(tempX, transform.localPosition.y,1);
        }).SetId(777);
        grillo.intId = 777;
    }

    public void atraparGrillo()
    {
        DOTween.Kill(grillo.intId);
        transform.localPosition = new Vector3(tempX, tempY, 1);
    }
}
