using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections.Generic;
using Unity.VisualScripting;

public class LoadSceneOnClick : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private float transitionDuration = 1f;
    [SerializeField] private List<GameObject> GODOWNOBJECTS = new List<GameObject>();
    [SerializeField] private List<GameObject> GOUPOBJECTS = new List<GameObject>();
    [SerializeField] CanvasGroup canvasGroup;
    private bool onAnim = false;


    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(StartSceneTransition);
        }
        else
        {
            Debug.LogError("LoadSceneOnClick: No se encontró el componente Button.");
        }
    }

    public void StartSceneTransition()
    {
        SoundManager.instance.playOnce(SoundManager.SFXS.popSfx2);
        if (onAnim)
        {
            return;
        }
        onAnim = true;

        if (GOUPOBJECTS.Count != 0)
        {
            for (int i = 0; i < GOUPOBJECTS.Count; i++)
            {
                GameObject g = GOUPOBJECTS[i];
                float tempX = g.transform.localPosition.x; float tempY = g.transform.localPosition.y;
                if (i == GOUPOBJECTS.Count - 1)
                {
                    g.transform.DOLocalMove(new Vector3(tempX, tempY + 1000, 0), transitionDuration).SetEase(Ease.InExpo).OnComplete(LoadNextScene);
                }
                else
                {
                    g.transform.DOLocalMove(new Vector3(tempX, tempY + 1000, 0), transitionDuration).SetEase(Ease.InExpo);
                }
            }
        }

        if (GODOWNOBJECTS.Count != 0)
        {
            for (int i = 0; i < GODOWNOBJECTS.Count; i++)
            {
                GameObject g = GODOWNOBJECTS[i];
                float tempX = g.transform.localPosition.x; float tempY = g.transform.localPosition.y;
                if(i == GODOWNOBJECTS.Count - 1)
                {
                    g.transform.DOLocalMove(new Vector3(tempX, tempY - 1000, 0), transitionDuration).SetEase(Ease.InExpo).OnComplete(LoadNextScene);
                }
                else
                {
                    g.transform.DOLocalMove(new Vector3(tempX, tempY - 1000, 0), transitionDuration).SetEase(Ease.InExpo);
                }
            }
        } 

        if (canvasGroup != null)
        {
            canvasGroup.DOFade(0f, transitionDuration + 1f).OnComplete(LoadNextScene);
        } 

        if (canvasGroup == null && GODOWNOBJECTS.Count == 0 && GOUPOBJECTS.Count == 0) {
        
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Cargar la nueva escena después de la transición
        SceneManager.LoadScene(sceneToLoad);
    }
}


