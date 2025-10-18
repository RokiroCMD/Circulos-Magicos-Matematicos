using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IUNitro : MonoBehaviour
{
    public float velocidadMovimientoNitro = 8.7f;
    public Transform[] puntosDeReferenciaNitro;
    public float tiempoEntreAparicionesNitro = 9.5f;
    public GameObject prefabNitro;
    public List<GameObject> nitroEnPantalla = new List<GameObject>();
    private float tiempoTranscurridoNitro = 0f;

    private void Start()
    {
        // Genera el primer nitro inmediatamente y luego repite cada tiempoEntreAparicionesNitro segundos
        GenerarNitro();
    }
    void Update()
    {
        if (GameManagerScript.Instance.getState() == GameManagerScript.GameState.PLAYING)
        {
            tiempoTranscurridoNitro += Time.deltaTime;

            if (tiempoTranscurridoNitro >= tiempoEntreAparicionesNitro)
            {
                GenerarNitro();
                tiempoTranscurridoNitro = 0f;
            }

            MoverNitro(); // Agrega esta línea para mover los nitros en la escena
        }
    }

    void GenerarNitro()
    {
        // Verifica si el prefabNitro no es nulo antes de generar nitro
        if (prefabNitro != null)
        {
            if (prefabNitro != null)
        {
            int indicePunto = Random.Range(0, puntosDeReferenciaNitro.Length);
            Vector3 posicionAleatoria = new Vector3(prefabNitro.transform.position.x, puntosDeReferenciaNitro[indicePunto].position.y, prefabNitro.transform.position.z);

            GameObject nitroActual = Instantiate(prefabNitro, posicionAleatoria, Quaternion.identity);
            nitroActual.transform.SetParent(prefabNitro.transform.parent);
            nitroActual.transform.localScale = Vector3.one;

            nitroEnPantalla.Add(nitroActual);

            // Elimina el nitro más antiguo si hay más de 3 en pantalla
            if (nitroEnPantalla.Count > 3)
            {
                Destroy(nitroEnPantalla[0]);
                nitroEnPantalla.RemoveAt(0);
            }
        }
        }
        else
        {
            Debug.LogError("El prefabNitro no ha sido asignado en el Inspector.");
        }
    }

    void MoverNitro()
    {
        for (int i = 0; i < nitroEnPantalla.Count; i++)
        {
            nitroEnPantalla[i].transform.Translate(Vector3.left * velocidadMovimientoNitro * Time.deltaTime);

            if (nitroEnPantalla[i].transform.position.x < -3000f)
            {
                Destroy(nitroEnPantalla[i]);
                nitroEnPantalla.RemoveAt(i);
                i--;
            }
        }
    }
}
