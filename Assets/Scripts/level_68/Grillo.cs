using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorGrillos : MonoBehaviour
{
    public float velocidadMovimientoGrillo = 8.7f;
    public Transform[] puntosDeReferenciaGrillo;
    public float tiempoEntreAparicionesGrillo = 9.5f;
    public GameObject prefabGrillo;
    public List<GameObject> grillosEnPantalla = new List<GameObject>();
    private float tiempoTranscurridoGrillo = 0f;

    private void Start()
    {
        // Genera el primer grillo inmediatamente y luego repite cada tiempoEntreAparicionesGrillo segundos
        GenerarGrillo();
    }

    void Update()
    {
        // Verifica si el juego está en estado de juego
        if (GameManagerScript.Instance.getState() == GameManagerScript.GameState.PLAYING)
        {
            tiempoTranscurridoGrillo += Time.deltaTime;

            // Verifica si ha pasado el tiempo necesario para generar un nuevo grillo
            if (tiempoTranscurridoGrillo >= tiempoEntreAparicionesGrillo)
            {
                GenerarGrillo();
                tiempoTranscurridoGrillo = 0f;
            }

            // Mueve los grillos en la escena
            MoverGrillos();

        }
    }

    void GenerarGrillo()
    {
        if (prefabGrillo != null)
        {

            int indicePunto = Random.Range(0, puntosDeReferenciaGrillo.Length);
            Vector3 posicionAleatoria = new Vector3(prefabGrillo.transform.position.x, puntosDeReferenciaGrillo[indicePunto].position.y, prefabGrillo.transform.position.z);

            GameObject grilloActual = Instantiate(prefabGrillo, posicionAleatoria, Quaternion.identity);
            grilloActual.transform.SetParent(prefabGrillo.transform.parent);
            grilloActual.transform.localScale = Vector3.one;

            grillosEnPantalla.Add(grilloActual);

            if (grillosEnPantalla.Count > 3)
            {
                Destroy(grillosEnPantalla[0]);
                grillosEnPantalla.RemoveAt(0);
            }
        }
        else
        {
            Debug.LogError("El prefabGrillo no ha sido asignado en el Inspector.");
        }
        SoundManager.instance.playOnce(SoundManager.SFXS.Cricri);
    }

    void MoverGrillos()
    {
        for (int i = 0; i < grillosEnPantalla.Count; i++)
        {
            grillosEnPantalla[i].transform.Translate(Vector3.left * velocidadMovimientoGrillo * Time.deltaTime);

            if (grillosEnPantalla[i].transform.position.x < -3000f)
            {
                Destroy(grillosEnPantalla[i]);
                grillosEnPantalla.RemoveAt(i);
                i--;
            }
        }
    }
}