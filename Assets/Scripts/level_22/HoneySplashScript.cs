using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using static SoundManager;

public class HoneySplashScript : MonoBehaviour
{
    [SerializeField] GameObject HoneyScreen;

    bool isSplashed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Bee"))
        {
            BeeScript bee = collision.gameObject.GetComponent<BeeScript>();
            if (bee.state == BeeScript.BeeState.MOVING)
            {

                if (ValuesGeneratorScripts.Instance.isValidValue(bee.num))
                {
                    SoundManager.instance.playOnce(SoundManager.SFXS.levelup);
                    bee.trapToHive();
                    if (bee.type == BeeScript.BeeType.NORMAL)
                    {
                        Level22Controller.Instance.scoreScript.addScore(1);
                    }
                    else if (bee.type == BeeScript.BeeType.DOUBLE)
                    {
                        Level22Controller.Instance.scoreScript.addScore(3);
                        SoundManager.instance.playOnce(SFXS.levelup2);
                    }
                }
                else
                {
                    bee.trapToHive();
                    if (bee.type == BeeScript.BeeType.DOUBLE)
                    {
                        Level22Controller.Instance.scoreScript.addScore(2);
                        SoundManager.instance.playOnce(SFXS.levelup2);
                    }
                    showSplashHoneyScreen();
                }

            }

        }
        else if (collision.gameObject.tag.Equals("Grillo"))
        {
            TimerScript.instance.addTime(5);
            SoundManager.instance.playOnce(SFXS.levelup2);
            SoundManager.instance.playOnce(SFXS.critdoble);
            CricketEvent.instance.atraparGrillo();
        }
    }

    public void showSplashHoneyScreen()
    {
        if (!isSplashed)
        {
            isSplashed = true;
            HoneyScreen.SetActive(true);
            SoundManager.instance.playOnce(SFXS.splat);
            HoneyScreen.transform.DOScale(Vector3.one, 0.2f);
            HoneyScreen.transform.DOScale(Vector3.zero, 1.2f).SetDelay(2f).OnComplete(() =>
            {
                isSplashed = false;
                HoneyScreen.SetActive(false);
            });
        }
        
    }

}
