
using UnityEngine;
using DG.Tweening;
using static SoundManager;
public class cuacha : MonoBehaviour
{
    Vector3 posicionInicial;
    float Y;
    private Animator animacion;
    bool cayendo = false;
        void Start()
    {
        animacion = GetComponent<Animator>();
        posicionInicial = transform.localPosition;
        Y = posicionInicial.y;
    }

    public  void  dejarCaer(){
    transform.DOLocalMoveY(90,0.33f).OnComplete(()=>{
        animacion.SetBool("Aterrizado",true);
        instance.playOnce(SFXS.cuacha);
        });
    }

    public void limpiar(){
    transform.localPosition = new Vector3(posicionInicial.x,800,posicionInicial.z);
    animacion.SetBool("Aterrizado",false);
    }
}
