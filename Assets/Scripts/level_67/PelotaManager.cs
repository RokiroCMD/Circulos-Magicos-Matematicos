using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using static SoundManager;
public class PelotaManager : MonoBehaviour
{
    public float factor = 0.2f;
    public int poderDeSalto;
    Vector3 posicionInicial;
    Vector3 escalaInicial;
    Camera camara;
    Vector3 orillaIzquierda;
    Vector3 orillaDerecha;
    TentaculoManager Pulpo;
    Transform bg;
    Collider2D colTemporizador;

    bool Seleccionable = true;
    void Start()
    {
        colTemporizador = GetComponent<Collider2D>();
        bg = GameObject.Find("Gameplay").transform;
        camara = Camera.main;
        Pulpo = GameObject.Find("Gameplay/cuerpopulpote").GetComponent<TentaculoManager>();
        posicionInicial = transform.localPosition;
        escalaInicial = transform.localScale;
        orillaIzquierda = bg.InverseTransformPoint(camara.ViewportToWorldPoint(new Vector3(0f-factor, 0.6f-factor, camara.nearClipPlane)));
        orillaDerecha = bg.InverseTransformPoint(camara.ViewportToWorldPoint(new Vector3(1f+factor, 0.6f-factor, camara.nearClipPlane)));
    }

    void Update(){
        Toque();
    }

   public void crearPelota(SalidaPelota salida){
        Seleccionable = true;
        switch (salida){
            case SalidaPelota.Izquierda:
            transform.localPosition = new Vector3(orillaIzquierda.x,orillaIzquierda.y,posicionInicial.z);
            Sequence girarIzquierda = DOTween.Sequence().Append(transform.DORotate(new Vector3(0, 0, -1080), 10f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear))
            .SetLoops(-1, LoopType.Incremental); 
            Sequence moverizq = transform.DOLocalJump(
            endValue: new Vector3(orillaDerecha.x+factor,orillaDerecha.y,posicionInicial.z),
            numJumps: 7,
            duration: 5f, 
            jumpPower:poderDeSalto).SetEase(Ease.Linear).OnComplete(()=>{girarIzquierda.Kill();});
            moverizq.Play();
            girarIzquierda.Play();
            break;

            case SalidaPelota.Derecha:
            Sequence girarDerecha = DOTween.Sequence().Append(transform.DORotate(new Vector3(0, 0, 1080), 10f, RotateMode.FastBeyond360));
            transform.localPosition = new Vector3(orillaDerecha.x+factor,orillaDerecha.y,posicionInicial.z);
            Sequence moverder = transform.DOLocalJump(
            endValue: new Vector3(orillaIzquierda.x,orillaIzquierda.y,posicionInicial.z),
            numJumps: 7,
            duration: 5f, 
            jumpPower:poderDeSalto).SetEase(Ease.Linear).OnComplete(()=>{girarDerecha.Kill();});
            moverder.Play();
            girarDerecha.Play();
            break;
        }

    }

   public enum SalidaPelota{
        Izquierda,
        Derecha
    }


 private void Toque()
    {
        if(Input.touchCount > 0){

        }
        if (Input.touchCount > 0 && Pulpo.estado == TentaculoManager.EstadoPulpo.SinObjetivo && GameManagerScript.Instance.getState() == GameManagerScript.GameState.PLAYING)
        {

            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:

                    Collider2D colision = Physics2D.OverlapPoint(touchPosition);
                    if (colTemporizador == colision && Seleccionable)
                    {
                        Pulpo.seguirMarcador(transform, false);
                        
                        destruirTemporizador();
                    }
                    break;
                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:
                    break;
            }
        } 
    }

    void destruirTemporizador()
    {
        instance.playOnce(SFXS.pelota);
        instance.playOnce(SFXS.correct);
        TimerScript.instance.addTime(5);
        Seleccionable = false;
        transform.DOPunchScale(
            punch:new Vector3(escalaInicial.x*1.2f,escalaInicial.y*1.2f,escalaInicial.z),
            duration: 0.5f
        ).OnComplete(()=>{
            transform.localPosition = orillaIzquierda;
        });
        
    }


}


