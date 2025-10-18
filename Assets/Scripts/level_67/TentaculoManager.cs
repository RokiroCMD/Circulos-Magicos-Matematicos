

using System;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using static SoundManager;
using Random = UnityEngine.Random;

public class TentaculoManager : MonoBehaviour
{


    private Vector2 posInicialIzq;
    private Vector2 posInicialDer;
    private Vector2 escalaInicialIzq;
    private Vector2 escalaInicialDer;
    private Quaternion rotacionInicialIzq;
    private Quaternion rotacionInicialDer;
    public float factorEstiramiento;
    [SerializeField] GameObject tentaculoDerecho;
    [SerializeField] GameObject tentaculoIzquierdo;
    private Animator animatorIzq;
    private Animator animatorDer;
    private Animator animatorPulpo;
    private SpriteRenderer spriteRendererIzq;
    private SpriteRenderer spriteRendererDer;
    public EstadoPulpo estado = EstadoPulpo.SinObjetivo;
    [SerializeField] GameObject particulas;
    Sequence animacionTentaculoDer;
    Sequence animacionTentaculoIzq;
    Transform bg;
    public UnityEvent llamarACuacha = new UnityEvent();
    public UnityEvent limpiarse = new UnityEvent();
    void Start()
    {
        animatorPulpo = GetComponent<Animator>();
        bg = GameObject.Find("Gameplay").transform;
        posInicialIzq = tentaculoIzquierdo.transform.localPosition;
        rotacionInicialIzq = tentaculoIzquierdo.transform.rotation;
        animatorIzq = tentaculoIzquierdo.GetComponent<Animator>();
        spriteRendererIzq = tentaculoIzquierdo.GetComponent<SpriteRenderer>();
        escalaInicialIzq = tentaculoIzquierdo.transform.localScale;

        posInicialDer = tentaculoDerecho.transform.localPosition;
        rotacionInicialDer = tentaculoDerecho.transform.rotation;
        animatorDer = tentaculoDerecho.GetComponent<Animator>();
        spriteRendererDer = tentaculoDerecho.GetComponent<SpriteRenderer>();
        escalaInicialDer = tentaculoDerecho.transform.localScale;
        animTentaculos();
        llamarACuacha.AddListener(GameObject.FindGameObjectWithTag("Cuacha").GetComponent<cuacha>().dejarCaer);
        limpiarse.AddListener(GameObject.FindGameObjectWithTag("Cuacha").GetComponent<cuacha>().limpiar);
    }


    public void animTentaculos()
    {
        animacionTentaculoDer = DOTween.Sequence();
        animacionTentaculoDer.Append(tentaculoDerecho.transform.DOScaleX(escalaInicialDer.x + 15, 1f));
        animacionTentaculoDer.Append(tentaculoDerecho.transform.DOScaleX(escalaInicialDer.x, 1f));
        animacionTentaculoDer.SetLoops(100, LoopType.Yoyo);
        animacionTentaculoDer.Play();

        animacionTentaculoIzq = DOTween.Sequence();
        animacionTentaculoIzq.Append(tentaculoIzquierdo.transform.DOScaleX(escalaInicialIzq.x + 15, 1f));
        animacionTentaculoIzq.Append(tentaculoIzquierdo.transform.DOScaleX(escalaInicialIzq.x, 1f));
        animacionTentaculoIzq.SetLoops(100, LoopType.Yoyo);
        animacionTentaculoIzq.Play();

    }

    public void detenerAnim()
    {
        animacionTentaculoDer.Kill();
        animacionTentaculoIzq.Kill();
    }



    public void seguirMarcador(Transform objeto, bool Trampa)
    {
        if (objeto != null && estado == EstadoPulpo.SinObjetivo)
        {
           GameObject particula = Instantiate(particulas,bg);
           particula.transform.localPosition = objeto.localPosition;
            if (objeto.localPosition.x < transform.localPosition.x)
            {
            estado = EstadoPulpo.BuscandoObjetivo;
                detenerAnim();
                animatorPulpo.SetInteger("Estado", 1);
                Vector2 posicionTentaculo = posInicialIzq;
                Vector2 posicionmarcador = objeto.localPosition;
                Vector2 deltaPosicion = posicionmarcador - posicionTentaculo;
                float factorEstiramientoActual = Mathf.Abs(deltaPosicion.x) * factorEstiramiento;
                Debug.Log("Buscando objeto");
                tentaculoIzquierdo.transform.DOScaleX(factorEstiramientoActual, 0.1f)
                .OnComplete(() =>
                {

                        estado = EstadoPulpo.Regresando;
                        Debug.Log("Regresando");

                    tentaculoIzquierdo.transform.DOScaleX(escalaInicialIzq.x, 0.1f).SetDelay(0.5f)
                    .OnComplete(() =>
                    {
                        if(Trampa == false){
                            animatorPulpo.SetInteger("Estado", 0);
                            estado = EstadoPulpo.SinObjetivo;
                        } else {
                            Aturdir(3000);
                            }
                        animTentaculos();
                    })

                    .OnPlay(() =>
                    {
                        animatorIzq.SetBool("Aplastando", false);
                        spriteRendererIzq.flipY = false;
                        tentaculoIzquierdo.transform.rotation = Quaternion.Euler(0f, 0f, 90f);

                        
                    });


                });
                float angulo = (Mathf.Atan2(deltaPosicion.y, deltaPosicion.x) * Mathf.Rad2Deg) + 360 % 360; ;
                tentaculoIzquierdo.transform.rotation = Quaternion.Euler(0f, 0f, angulo);
                animatorIzq.SetBool("Aplastando", true);
                if (angulo <= 90 && angulo >= -90)
                {

                    spriteRendererIzq.flipY = false;
                }
                else
                {
                    spriteRendererIzq.flipY = true;
                }

            }
            else
            {

                detenerAnim();
                animatorPulpo.SetInteger("Estado", 2);
                Vector2 posicionTentaculo = posInicialDer;
                Vector2 posicionmarcador = objeto.localPosition;
                Vector2 deltaPosicion = posicionmarcador - posicionTentaculo;
                float factorEstiramientoActual = Mathf.Abs(deltaPosicion.x) * factorEstiramiento;
                Debug.Log(factorEstiramientoActual);
                estado = EstadoPulpo.BuscandoObjetivo;
                tentaculoDerecho.transform.DOScaleX(factorEstiramientoActual, 0.1f)
                .OnComplete(() =>
                {
                    estado = EstadoPulpo.Regresando;
                    tentaculoDerecho.transform.DOScaleX(escalaInicialDer.x, 0.1f).SetDelay(0.5f)
                    .OnComplete( () =>
                    {
                        if(Trampa == false){
                            animatorPulpo.SetInteger("Estado", 0);
                            estado = EstadoPulpo.SinObjetivo;
                        } else {
                          Aturdir(3000);
                            }
                        animTentaculos();
                    })
                    .OnPlay(() =>
                    {
                        animatorDer.SetBool("Aplastando", false);
                        spriteRendererDer.flipY = false;
                        tentaculoDerecho.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                    });


                });
                float angulo = Mathf.Atan2(deltaPosicion.y, deltaPosicion.x) * Mathf.Rad2Deg;
                tentaculoDerecho.transform.rotation = Quaternion.Euler(0f, 0f, angulo);
                animatorDer.SetBool("Aplastando", true);

                if (angulo <= 90 && angulo >= -90)
                {

                    spriteRendererDer.flipY = false;
                }
                else
                {
                    spriteRendererDer.flipY = true;
                }



            }

        } else {Debug.Log("Nulo");}



    }



    public async Task Aturdir(int milisegundos){
        instance.playOnce(SFXS.gaviota);
        
        llamarACuacha.Invoke();
        estado = EstadoPulpo.Aturdido;
        await Task.Delay(milisegundos/3);
        Sequence sacudidaPulpo = DOTween.Sequence().Append(transform.DOShakePosition(milisegundos/2000,20));
        Sequence sacudidaDer = DOTween.Sequence().Append(tentaculoDerecho.transform.DOShakePosition(milisegundos/2000,20));
        Sequence sacudidaIzq = DOTween.Sequence().Append(tentaculoIzquierdo.transform.DOShakePosition(milisegundos/2000,20));
        sacudidaPulpo.Play();
        sacudidaDer.Play();
        sacudidaIzq.Play();
        animatorPulpo.SetInteger("Estado", 3);
        instance.playOnce(SFXS.mareado);
        await Task.Delay(milisegundos/3);
        animatorPulpo.SetInteger("Estado", 4);
        int r = Random.Range(0,2);
        if (r == 0) animatorDer.SetBool("Limpiando", true); else animatorIzq.SetBool("Limpiando", true);
        instance.playOnce(SFXS.limpieza);
        limpiarse.Invoke();
        await Task.Delay(milisegundos/3);
                animatorPulpo.SetInteger("Estado", 0);
                estado = EstadoPulpo.SinObjetivo;
                animatorDer.SetBool("Limpiando", false);
                animatorIzq.SetBool("Limpiando", false);
        sacudidaPulpo.Kill();
        sacudidaDer.Kill();
        sacudidaIzq.Kill();
        

    }
    public enum EstadoPulpo
    {
        SinObjetivo, BuscandoObjetivo, Regresando, Aturdido

    }


}

