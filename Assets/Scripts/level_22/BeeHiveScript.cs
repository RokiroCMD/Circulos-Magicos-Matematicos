using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static SoundManager;

public class BeeHiveScrpit : MonoBehaviour
{
    bool isInAnim = false;
    Vector3 punchScale;
    [SerializeField] GameObject honeySplash;
    
    private void Start()
    {
        punchScale = gameObject.transform.localScale;
        punchScale *= 0.3f;
    }

    public void animateHive()
    {
        if (!isInAnim)
        {
            SoundManager.instance.playOnce(SFXS.popSfx1);
            isInAnim = true;
            honeySplash.SetActive(true);
            gameObject.transform.DOPunchScale(punchScale, 0.3f).OnComplete(() => { 
                isInAnim=false;
                honeySplash.SetActive(false);
            });
        }
    }
}
