using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScriptMontoncito : MonoBehaviour
{
    private Vector2 EscalaInicial;
        private Vector3 posicionInicial;
    void Awake()
    {

        float posicionZPadre = transform.parent.position.z;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posicionZPadre + 1f);
        EscalaInicial = transform.localScale;
        posicionInicial = transform.localPosition;
      //  transform.position = new Vector3(posicionInicial.x,posicionInicial.y, posicionInicial.z);
        transform.localScale = new Vector2(EscalaInicial.x,0);
        transform.DOScaleY(EscalaInicial.y,0.2f).OnComplete(()=>{
            
                        Sequence sec = DOTween.Sequence()
            .Append(
                transform.DOScaleX(EscalaInicial.x+Random.Range(-0.5f,0.5f),0.2F))            
            .Append(
                 transform.DOScaleX(EscalaInicial.x+Random.Range(-0.5f,0.5f),0.2F)
            );

            

            sec.Play().OnComplete(()=>{
            transform.DOScaleY(0,0.2F).OnComplete(()=>{                
                Destroy(gameObject);});
});
        });


    }

}
