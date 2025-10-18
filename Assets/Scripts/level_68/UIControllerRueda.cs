using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class UIControllerRueda : MonoBehaviour
{
    [SerializeField] private GameManagerScript gameManagerScript;
    [SerializeField] private StartScreenScript startScreenScript;
    public float velocidadMovimientoCone = 5f;
    public Transform[] puntosDeReferencia;
    public float tiempoEntreApariciones = 2f;
    public GameObject prefabCono;
    public List<GameObject> conosEnPantalla = new List<GameObject>();
    private int conosAparecidos = 0;
    private GameObject conoActual;


    private float tiempoTranscurrido = 0f;

    [SerializeField] private RectTransform llanta;
    [SerializeField] private RectTransform Hijollanta;

    [SerializeField] private RectTransform[] divisiones;
    [SerializeField] private float velocidadMovimiento = 5f;
    public float rotationSpeed = 700f;
    private int divisionActual = 2;
    private bool enMovimiento = false;

    [SerializeField] public float parallax_speedCloud = .05f;
    [SerializeField] private RawImage parallaxCloud;

    [SerializeField] public float parallax_speedBuilding = .2f;
    [SerializeField] private RawImage parallaxBuilding;

    [SerializeField] public float parallax_speedStreet = .4f;
    [SerializeField] private RawImage parallaxStreet;

    private PauseScript _pauseScript;
    private TimerScript timerScript;
    private ScoreScript scoreScript;
    private FinishScript finishScript;

    private bool isPaused = false;

    private void Start()
    {
        _pauseScript = PauseScript.instance;
        timerScript = TimerScript.instance;
        gameManagerScript = GameManagerScript.Instance;
        scoreScript = ScoreScript.Instance;

        startScreenScript = GetComponent<StartScreenScript>();
        _pauseScript.OnPauseEvent.AddListener(OnPause);
        timerScript.TimeChangeEvent.AddListener(onTimeChanged);
        gameManagerScript.StateChangedEvent.AddListener(OnManager);
        startScreenScript.ShowStartScreen();

        gameManagerScript.changeState(GameManagerScript.GameState.STARTING);
        finishScript = FindObjectOfType<FinishScript>();
    }

    public void RestartScene()
    {
        int indiceEscenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indiceEscenaActual);
    }

    public void StartGame()
    {
        startScreenScript.HideStartScreen();
    }

    private void OnPause(bool pause)
    {
        isPaused = pause;
        if (pause)
        {
            gameManagerScript.changeState(GameManagerScript.GameState.PAUSE);
            timerScript.stopTimer();
        }
        else
        {
            gameManagerScript.changeState(GameManagerScript.GameState.PLAYING);
            timerScript.startTimer();
        }
    }

    private void onTimeChanged(int time)
    {
        if (time >= timerScript.maxTime)
        {
            gameManagerScript.changeState(GameManagerScript.GameState.ENDING);
        }
    }

    private void OnManager(GameManagerScript.GameState state)
    {
        switch (state)
        {
            case GameManagerScript.GameState.STARTING:
                break;
            case GameManagerScript.GameState.PLAYING:
                break;
            case GameManagerScript.GameState.PAUSE:
                break;
            case GameManagerScript.GameState.ENDING:
                break;
        }
    }

    private void Update()
    {
        if (gameManagerScript.getState() == GameManagerScript.GameState.PLAYING)
        {
            correrJuego();
        }
    }

    private void correrJuego()
    {
        RotarRueda();
        ActualizarParallax();

        tiempoTranscurrido += Time.deltaTime;

        if (tiempoTranscurrido >= tiempoEntreApariciones)
        {
            GenerarCono();
            tiempoTranscurrido = 0f;
        }

        MoverConos();
    }

    void CambiarDivisionYMoverLlanta(int nuevaDivision)
    {
        if (nuevaDivision == divisionActual)
            return;

        enMovimiento = true;
        divisionActual = nuevaDivision;

        float posY = divisiones[nuevaDivision].localPosition.y;
        llanta.transform.DOLocalMove(new Vector3(llanta.localPosition.x, posY, 0), 0.35f).OnComplete(() =>
        {
            enMovimiento = false;
        }).SetEase(Ease.OutBounce);
    }

    private void RotarRueda()
    {
        Hijollanta.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }

    private void ActualizarParallax()
    {
        parallaxCloud.uvRect = new Rect(parallaxCloud.uvRect.position + new Vector2(parallax_speedCloud, 0) * Time.deltaTime, parallaxCloud.uvRect.size);
        parallaxBuilding.uvRect = new Rect(parallaxBuilding.uvRect.position + new Vector2(parallax_speedBuilding, 0) * Time.deltaTime, parallaxBuilding.uvRect.size);
        parallaxStreet.uvRect = new Rect(parallaxStreet.uvRect.position + new Vector2(parallax_speedStreet, 0) * Time.deltaTime, parallaxStreet.uvRect.size);
    }

    public void Subir()
    {
        if (enMovimiento)
            return;
        int nuevaDivision = Mathf.Max(divisionActual - 1, 0);
        CambiarDivisionYMoverLlanta(nuevaDivision);
    }

    public void Bajar()
    {
        if (enMovimiento)
            return;
        int nuevaDivision = Mathf.Min(divisionActual + 1, divisiones.Length - 1);
        if (nuevaDivision == divisionActual)
        {
            return;
        }
        CambiarDivisionYMoverLlanta(nuevaDivision);
    }

    void GenerarCono()
    {
        if (prefabCono != null)
        {
            int indicePunto = UnityEngine.Random.Range(0, puntosDeReferencia.Length);
            Vector3 posicionAleatoria = new Vector3(prefabCono.transform.position.x, puntosDeReferencia[indicePunto].position.y, prefabCono.transform.position.z);

            GameObject conoActual = Instantiate(prefabCono, posicionAleatoria, Quaternion.identity);
            conoActual.transform.SetParent(prefabCono.transform.parent);
            conoActual.transform.localScale = Vector3.one;

            conosEnPantalla.Add(conoActual);

            if (conosEnPantalla.Count > 4)
            {
                Destroy(conosEnPantalla[0]);
                conosEnPantalla.RemoveAt(0);
            }
        }
        else
        {
            Debug.LogError("El prefabCono no ha sido asignado en el Inspector.");
        }
    }

    void MoverConos()
    {
        for (int i = 0; i < conosEnPantalla.Count; i++)
        {
            conosEnPantalla[i].transform.Translate(Vector3.left * velocidadMovimientoCone * Time.deltaTime);

            if (conosEnPantalla[i].transform.position.x < -3000f)
            {
                Destroy(conosEnPantalla[i]);
                conosEnPantalla.RemoveAt(i);
                i--;
            }
        }
    }


}