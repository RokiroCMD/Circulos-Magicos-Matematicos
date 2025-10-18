using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    private PauseScript _pauseScript;
    public float velocidadMovimientoMesh = 5f;
    public Transform[] puntosDeReferenciaMesh;
    public float tiempoEntreAparicionesMesh = 2f;
    public GameObject prefabMesh;
    public int poolSize = 10; 
    private List<GameObject> meshPool = new List<GameObject>();
    private int poolIndex = 0; 
    private float tiempoTranscurridoMesh = 0f;
    private bool isPaused = false;

    private int[] numeros = { 6, 8, 14, 48 };
    private string[] numerosFlotantes = { "6.0", "8.0", "14.0", "48.0" };

    [SerializeField] private TextMeshProUGUI meshText;

    private void Start()
    {
        _pauseScript = PauseScript.instance;
        _pauseScript.OnPauseEvent.AddListener(OnPause);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefabMesh);
            obj.SetActive(false);
            meshPool.Add(obj);
        }
    }

    private void OnPause(bool pause)
    {
        isPaused = pause;
    }

    void Update()
    {
        if (GameManagerScript.Instance.getState() == GameManagerScript.GameState.PLAYING && !isPaused)
        {
            tiempoTranscurridoMesh += Time.deltaTime;

            if (tiempoTranscurridoMesh >= tiempoEntreAparicionesMesh)
            {
                GenerarMesh();
                tiempoTranscurridoMesh = 0f;
            }

            MoverMesh();
        }
    }

    void GenerarMesh()
    {
        int indicePunto = Random.Range(0, puntosDeReferenciaMesh.Length);
        Vector3 posicionAleatoria = new Vector3(prefabMesh.transform.position.x, puntosDeReferenciaMesh[indicePunto].position.y, prefabMesh.transform.position.z);

        GameObject meshObject = GetObjectFromPool();
        meshObject.transform.position = posicionAleatoria;
        meshObject.SetActive(true);

        // Asegurarse de establecer el padre correctamente
        meshObject.transform.SetParent(prefabMesh.transform.parent);

        // Restablecer la escala a Vector3.one para evitar problemas de escala heredada
        meshObject.transform.localScale = Vector3.one;

        TextMeshProUGUI textoMesh = meshObject.GetComponentInChildren<TextMeshProUGUI>();
        if (textoMesh != null)
        {
            string numeroAleatorio;
            if (Random.Range(0, 10) == 0)
            {
                int indiceFlotante = Random.Range(0, numerosFlotantes.Length);
                numeroAleatorio = numerosFlotantes[indiceFlotante].ToString();
                textoMesh.color = Color.yellow;
                textoMesh.outlineColor = Color.black;
            }
            else
            {
                int indiceEntero = Random.Range(0, numeros.Length);
                numeroAleatorio = numeros[indiceEntero].ToString();
                textoMesh.color = Color.magenta;
                textoMesh.outlineColor = Color.black;
            }
            textoMesh.text = numeroAleatorio;
        }
    }



    GameObject GetObjectFromPool()
    {
        GameObject obj = meshPool[poolIndex];
        poolIndex = (poolIndex + 1) % poolSize; 
        return obj;
    }

    void MoverMesh()
    {
        for (int i = 0; i < meshPool.Count; i++)
        {
            if (meshPool[i].activeSelf)
            {
                meshPool[i].transform.Translate(Vector3.left * velocidadMovimientoMesh * Time.deltaTime);

                if (meshPool[i].transform.position.x < -3000f)
                {
                    meshPool[i].SetActive(false);
                }
            }
        }
    }
}
