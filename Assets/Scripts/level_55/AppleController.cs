using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AppleController : MonoBehaviour
{

    [SerializeField] public List<Collider2D> apples;

    public List<int> appleValues = new List<int>();

    public static AppleController Instance;
    private ValuesGeneratorScripts valuesGenerator;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        Instance = this;
    }
    private void Start()
    {
        valuesGenerator = ValuesGeneratorScripts.Instance;
    }

    public void FillAppleValues()
    {
        valuesGenerator.FillValues();
        appleValues = new List<int>(valuesGenerator.values);
        
    }

    public void ThrowApples()
    {
        //DOTween.CompleteAll();
        ScoreScript.Instance.removeScore(1);
        for (int i = 0; i < apples.Count; ++i)
        {
            AppleScript apple = apples[i].gameObject.GetComponent<AppleScript>();
            if(i == apples.Count - 1)
            {
                apple.fall(true);
            }
            else
            {
                apple.fall(false);
            }
        }
    }

    public void RespawnApples()
    {
        FillAppleValues();
        GenerateApples();
    }

    private int getRandomAppleValue()
    {
        int indiceAleatorio = Random.Range(0, appleValues.Count);

        // Obtener y mostrar el valor aleatorio
        int valorAleatorio = appleValues[indiceAleatorio];
        appleValues.RemoveAt(indiceAleatorio);
        return valorAleatorio;
    }

    public void GenerateApples()
    {

        for (int i = 0; i < apples.Count; ++i)
        {
            AppleScript apple = apples[i].gameObject.GetComponent<AppleScript>();
            apple.stopAnims();
            apple.setNum(getRandomAppleValue());
            apple.Relocate();
            apple.Shake();
        }
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {

            if (GameManagerScript.Instance.getState() !=  GameManagerScript.GameState.PLAYING)  return; 

            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (apples.Count > 0)
                    {
                        RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                        if (hit.collider != null)
                        {
                            if (hit.transform.tag == "Grillo")
                            {
                                CricketEventApple.instance.atraparGrillo();
                                SoundManager.instance.playOnce(SoundManager.SFXS.levelup2);
                                TimerScript.instance.addTime(5);
                            }
                            else
                            {
                                for (int i = 0; i < apples.Count; i++)
                                {

                                    if (hit.collider == apples[i])
                                    {
                                        apples[i].gameObject.GetComponent<AppleScript>().touch();
                                    }
                                }
                            }
                        }
                    }
                    
                    break;
            }
        }    
    }
}
