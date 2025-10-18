using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CangrejoManager : MonoBehaviour
{
    [SerializeField] GameObject cangrejo;
    private List<int> ints;
    public List<GameObject> cangrejos;
    public GameObject canvas;
    private List<int> numerosMagicos;
    public List<int> ListaNumeros;
    private ValuesGeneratorScripts valuesGeneratorScripts;
    void Awake()
    {
        cangrejos = new List<GameObject>();
    }

    private void Start()
    {
        valuesGeneratorScripts = ValuesGeneratorScripts.Instance;
    }

    public void LlamarCangrejos()
    {
        cangrejos = new List<GameObject>();
        int lado = -1;
        int renumCangrejos = 0;

        valuesGeneratorScripts.FillValues();
        ListaNumeros = new List<int>(valuesGeneratorScripts.values);
        for (int i = 0; i < 4; i++)
        {

            for (int y = 0; y < 2; y++)
            {
                renumCangrejos++;
                lado = lado * -1;
                GameObject cangrejin = Instantiate(cangrejo, canvas.transform);
                cangrejos.Add(cangrejin);
                cangrejin.transform.localPosition = new Vector3(250 * lado + UnityEngine.Random.Range(200, 600) * lado, -500 + (i * 200), -13);
                CangrejoControlador controlador = cangrejin.GetComponent<CangrejoControlador>();
                if (controlador == null) { Debug.Log("controlador no hallado"); }
                int sInt;

                sInt = valuesGeneratorScripts.getRandomValue();
                controlador.Tranpa = true;
                controlador.numero = sInt;
                if (valuesGeneratorScripts.isValidValue(sInt))
                {
                    controlador.Tranpa = false;
                }
                
            }
        }
    }

    

    public void QuitarCangrejos()
    {
        foreach (GameObject cangrejo in cangrejos)
        {   
            if (cangrejo.transform.localPosition.x < 0)
            { 
                cangrejo.transform.DOLocalMoveX(-2000,0.2f).OnComplete(()=>{
                Destroy(cangrejo);
                });
            } else 
            {
                cangrejo.transform.DOLocalMoveX(2000,0.1f).OnComplete(()=>{
                Destroy(cangrejo);
                });
            }
            
        }

    }



    public void ChequearLista(int Numero){

            if (valuesGeneratorScripts.isValidValue(Numero))
            {
                Debug.Log("Cangrejo bueno");
                Debug.Log(ListaNumeros.Remove(Numero));
                ScoreScript.Instance.addScore(1);
                SoundManager.instance.playOnce(SoundManager.SFXS.popSfx1);
            } else  quitarYLlamarCangrejos(1000,2500);



        foreach(int numero in ListaNumeros){
            if (valuesGeneratorScripts.isValidValue(numero)){
                Debug.Log("Aun quedan cangrejos validos");
                return;
            }
        }
            quitarYLlamarCangrejos(500,500);

    }

    public async Task quitarYLlamarCangrejos(int milisegundosSalida, int milisegundosEntrada)
    {
        await Task.Delay(milisegundosSalida);
        QuitarCangrejos();
        await Task.Delay(milisegundosEntrada);
        LlamarCangrejos();
    }


}
