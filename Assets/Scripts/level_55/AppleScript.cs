using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Level55Controller;

public class AppleScript : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int num;
    public bool isActive = true;
    private Vector3 originPosition;
    private int id = 0;
    public static int ids =0;

    private void Awake()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        originPosition = transform.localPosition;
        id = getID();
    }
        

    private static int getID()
    {
        return ids++;
    }
    public void stopAnims()
    {
        DOTween.Kill(id);
    }

    public void setNum(int tmpNum)
    {
        num = tmpNum;
        text.text = num.ToString();
    }

    public void Relocate()
    {
        transform.localPosition= originPosition;
        transform.localScale = Vector3.one;
        isActive = true;
    }


    public void Shake()
    {
        transform.DOShakePosition(0.5f, 5f);
    }
    public void touch()
    {
        if (isActive && GameManagerScript.Instance.getState() == GameManagerScript.GameState.PLAYING)
        {
            if (ValuesGeneratorScripts.Instance.isValidValue(num))
            {
                SoundManager.instance.playOnce(SoundManager.SFXS.levelup);
                ScoreScript.Instance.addScore(1);
                isActive = false;
                Tween movingTween = transform.DOLocalMove(new Vector3(originPosition.x, +25, originPosition.z), 0.6f).SetEase(Ease.InExpo);
                Tween scalingTween = transform.DOScale(0.35f, 1.2f).OnComplete(() => {
                    ValuesGeneratorScripts.Instance.removeValue(num);
                    if (!ValuesGeneratorScripts.Instance.hasValuesLeft())
                    {
                        AppleController.Instance.RespawnApples();
                    }
                });

                movingTween.intId = id;
                scalingTween.intId = id;

            }
            else
            {
                AppleController.Instance.ThrowApples();
            }
        }
        
    }

    public void fall(bool restart)
    {
        isActive = false;
        transform.DOLocalMove(new Vector3(originPosition.x, -20, originPosition.z), 0.6f).SetEase(Ease.InExpo);
        if (restart)
        {
            transform.DOScale(0.12f, 1.2f).OnComplete(() => {
                    AppleController.Instance.RespawnApples();
            });
        }
        else
        {
            transform.DOScale(0.12f, 1.2f);
        }
        
    }
}
