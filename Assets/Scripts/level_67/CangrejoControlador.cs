
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CangrejoControlador : MonoBehaviour
{
    public GameObject tentaculo;
    
    private TentaculoManager tm;
    public GameObject numeroTexto;
    CangrejoManager cManager;
    private GameObject cm;
    JuegoManager gameManager;
    TimerScript timerScript;

    Collider2D col;
    public bool mov = false;
    public int numero;

    public bool Tranpa = false;
    public Vector2 EscalaInicial;

    public CangrejoControlador(int Numero)
    {
        numero = Numero;
    }

    void Awake()
    {       
        EscalaInicial = transform.localScale;
        transform.localScale = new Vector2(EscalaInicial.x,0);
        transform.DOScale(EscalaInicial, 0.2f);
        cm = GameObject.Find("---Managers---/CangrejoManager");
        tentaculo = GameObject.Find("Gameplay/cuerpopulpote");
        if (tentaculo != null) tm = tentaculo.GetComponent<TentaculoManager>();
        cManager = cm.GetComponent<CangrejoManager>();
        timerScript = TimerScript.instance;

    }

    void Start()
    {
        col = GetComponent<Collider2D>();
        
        TextMeshProUGUI texto = numeroTexto.GetComponent<TextMeshProUGUI>();
        if (texto != null)
        {
            texto.SetText("" + numero);
        }
        else
        {
        }
    }




    void Update()
    {
        Toque();
    }

    private void Toque()
    {
        if(Input.touchCount > 0){

        }
        if (Input.touchCount > 0 && tm.estado == TentaculoManager.EstadoPulpo.SinObjetivo && GameManagerScript.Instance.getState() == GameManagerScript.GameState.PLAYING)
        {

            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    Collider2D colision = Physics2D.OverlapPoint(touchPosition);
                    if (col == colision)
                    {
                        tm.seguirMarcador(transform, Tranpa);
                        cManager.cangrejos.Remove(gameObject);
                          cManager.ChequearLista(numero);
                        Destroy(gameObject);
                    }
                    break;
                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    break;
            }
        } 
    }



}
