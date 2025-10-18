using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static GameManagerScript;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject pausePanel;
    [SerializeField] List<GameObject> affectedPausedElements;
    private GameManagerScript gameManager;

    bool isPaused = false;

    public UnityEvent<bool> OnPauseEvent = new UnityEvent<bool>();

    public static PauseScript instance;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        gameManager = GameManagerScript.Instance;
        gameManager.StateChangedEvent.AddListener(onSateChanged);
    }

    public void onSateChanged(GameState state)
    {
        switch (state)
        {
            case GameState.PAUSE:
                Pause();
                break;
            case GameState.PLAYING:
                Resume();
                break;
            case GameState.ENDING:
                hideAffectedElements();
                break;
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {

            SoundManager.instance.playOnce(SoundManager.SFXS.popSfx2);
            isPaused = true;
            showPauseScreen();
        }
    }

    public void Resume()
    {
        if (isPaused) {

            SoundManager.instance.playOnce(SoundManager.SFXS.popSfx2);
            isPaused = false;
        hidePauseScreen();
        }
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

    public void showPauseScreen()
    {

        hideAffectedElements();


        pauseScreen.SetActive(true);
        CanvasGroup cgPause = pauseScreen.GetComponent<CanvasGroup>();

        pauseButton.transform.DOScale(0, 0.5f).SetEase(Ease.OutSine);
        //helpButton.transform.DOScale(0, 0.5f).SetEase(Ease.OutSine);

        pausePanel.transform.localPosition = new Vector3(0, 1500, 0);

        pausePanel.transform.localScale = new Vector3(0, 0, 0);

        pausePanel.transform.DOLocalMove(Vector3.zero, 0.8f).SetEase(Ease.OutSine);
        pausePanel.transform.DOScale(1, 0.8f).SetEase(Ease.OutBounce).OnComplete(() => {
            cgPause.interactable = true;
            cgPause.blocksRaycasts = true;
            OnPauseEvent.Invoke(true);
        });

    }


    public void hidePauseScreen()
    {
        CanvasGroup cgPause = pauseScreen.GetComponent<CanvasGroup>();
        pausePanel.transform.DOLocalMove(new Vector3(0, 1500, 0), 0.8f).SetEase(Ease.InOutSine);
        pausePanel.transform.DOScale(0, 0.8f)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => {
                cgPause.interactable = false;
                cgPause.blocksRaycasts = false;
                showAffectedElements();

                OnPauseEvent.Invoke(false);
                pauseScreen.SetActive(false);


            });


        pauseButton.transform.DOScale(1, 1f).SetEase(Ease.OutBounce);
        //helpButton.transform.DOScale(1, 1f).SetEase(Ease.OutBounce);
    }


    void Update()
    {
        
    }
}
