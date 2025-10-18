using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManagerScript;

public class StartScreenScript : MonoBehaviour
{

    GameManagerScript gameManager;
    [SerializeField] List<GameObject> affectedPausedElements;

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject startPanel;

    private void Start()
    {
        gameManager = GameManagerScript.Instance;
    }

    public void hideAffectedElements()
    {
        for (int i = 0; i < affectedPausedElements.Count; i++)
        {
            CanvasGroup cgAffected = affectedPausedElements[i].GetComponent<CanvasGroup>();
            cgAffected.blocksRaycasts = false;
            cgAffected.interactable = false;
        }
    }

    public void showAffectedElements()
    {
        for (int i = 0; i < affectedPausedElements.Count; i++)
        {
            CanvasGroup cgAffected = affectedPausedElements[i].GetComponent<CanvasGroup>();
            cgAffected.blocksRaycasts = true;
            cgAffected.interactable = true;
        }
    }


    public void ShowStartScreen()
    {
        hideAffectedElements();

        CanvasGroup cgStart = startScreen.GetComponent<CanvasGroup>();
        startScreen.SetActive(true);


        startPanel.transform.localScale = new Vector3(0, 0, 0);

        startPanel.transform.DOScale(1, 0.8f).SetEase(Ease.OutBounce).OnComplete(() => {
            cgStart.interactable = true;
            cgStart.blocksRaycasts = true;
        });
    }


    public void HideStartScreen()
    {
        CanvasGroup cgStart = startScreen.GetComponent<CanvasGroup>();
        cgStart.interactable = false;
        cgStart.blocksRaycasts = false;
        SoundManager.instance.playOnce(SoundManager.SFXS.popSfx2);
        startPanel.transform.DOScale(0, 0.8f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => {
                startScreen.SetActive(false);

                showAffectedElements();

                gameManager.changeState(GameState.PLAYING);
            });
    }
}
