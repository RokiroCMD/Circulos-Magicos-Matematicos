using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIColliderCheck : MonoBehaviour
{
    [SerializeField] private ScoreScript scoreScript;
    [SerializeField] private FinishScript finishScript;

    private UIControllerRueda controller;
    private UIMove meshController;
    private IUNitro nitroController;
    private GeneradorGrillos grilloController;

    private bool efectoActivo = false;

    private float incrementoVelocidad = 0.8f;
 

    private Dictionary<string, float> velocidadesOriginales = new Dictionary<string, float>();

    private int nitroCount = 0;

    void Start()
    {
        finishScript = FindObjectOfType<FinishScript>();
        controller = FindObjectOfType<UIControllerRueda>();
        meshController = FindObjectOfType<UIMove>();
        nitroController = FindObjectOfType<IUNitro>();
        grilloController = FindObjectOfType<GeneradorGrillos>();


        velocidadesOriginales.Add("parallax_speedCloud", controller.parallax_speedCloud);
        velocidadesOriginales.Add("parallax_speedBuilding", controller.parallax_speedBuilding);
        velocidadesOriginales.Add("parallax_speedStreet", controller.parallax_speedStreet);
        velocidadesOriginales.Add("rotationSpeed", controller.rotationSpeed);
        velocidadesOriginales.Add("velocidadMovimientoCone", controller.velocidadMovimientoCone);
        velocidadesOriginales.Add("velocidadMovimientoMesh", meshController.velocidadMovimientoMesh);
        velocidadesOriginales.Add("velocidadMovimientoNitro", nitroController.velocidadMovimientoNitro);
        velocidadesOriginales.Add("tiempoEntreAparicionesMesh", meshController.tiempoEntreAparicionesMesh);
        velocidadesOriginales.Add("tiempoEntreApariciones", controller.tiempoEntreApariciones);
        velocidadesOriginales.Add("velocidadMovimientoGrillo", grilloController.velocidadMovimientoGrillo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Cone"))
        {
            DisminuirVelocidad();
        }
        else if (other.CompareTag("Mesh"))
        {
            GameObject meshObject = other.gameObject;
            TextMeshProUGUI meshText = meshObject.GetComponentInChildren<TextMeshProUGUI>();

            string meshNumber = meshText.text;

            if (meshNumber.EndsWith(".0"))
            {
                ScoreScript.Instance.addScore(2);
                SoundManager.instance.playOnce(SoundManager.SFXS.popSfx1);
                meshObject.SetActive(false);
            }
            else
            {
                ScoreScript.Instance.addScore(1);
                SoundManager.instance.playOnce(SoundManager.SFXS.popSfx1);
                meshObject.SetActive(false);
            }
        }
        else if (other.CompareTag("nitro"))
        {
            SoundManager.instance.playOnce(SoundManager.SFXS.Notificacion02);
            efectoActivo = true;
            AumentarVelocidad();
            nitroCount++;
            other.gameObject.SetActive(false);
        }
        else if (other.CompareTag("Grillo"))
        {
            other.gameObject.SetActive(false);
            SoundManager.instance.playOnce(SoundManager.SFXS.Crujido);
            GameManagerScript.Instance.changeState(GameManagerScript.GameState.ENDING);
           
        }
    }

    private void AumentarVelocidad()
    {
        controller.parallax_speedCloud *= (1 + incrementoVelocidad);
        controller.parallax_speedBuilding *= (1 + incrementoVelocidad);
        controller.parallax_speedStreet *= (1 + incrementoVelocidad);
        controller.rotationSpeed *= (1 + incrementoVelocidad);
        controller.velocidadMovimientoCone *= (1 + incrementoVelocidad);
        meshController.velocidadMovimientoMesh *= (1 + incrementoVelocidad);
        nitroController.velocidadMovimientoNitro *= (1 + incrementoVelocidad);
        meshController.tiempoEntreAparicionesMesh /= (1 + incrementoVelocidad);
        controller.tiempoEntreApariciones /= (1 + incrementoVelocidad);
        
        grilloController.velocidadMovimientoGrillo *= (1 + incrementoVelocidad);
    }

    private void DisminuirVelocidad()
    {
        if (nitroCount > 0)
        {
            controller.parallax_speedCloud /= (1 + incrementoVelocidad);
            controller.parallax_speedBuilding /= (1 + incrementoVelocidad);
            controller.parallax_speedStreet /= (1 + incrementoVelocidad);
            controller.rotationSpeed /= (1 + incrementoVelocidad);
            controller.velocidadMovimientoCone /= (1 + incrementoVelocidad);
            meshController.velocidadMovimientoMesh /= (1 + incrementoVelocidad);
            nitroController.velocidadMovimientoNitro /= (1 + incrementoVelocidad);
            grilloController.velocidadMovimientoGrillo /= (1 + incrementoVelocidad);
            meshController.tiempoEntreAparicionesMesh *= (1 + incrementoVelocidad);
            controller.tiempoEntreApariciones *= (1 + incrementoVelocidad);
            nitroCount--;
        }
    }
}
