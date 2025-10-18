using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjusterScript : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private SpriteRenderer adjustBOX;
    private SpriteRenderer adjustBOXRenderer;
    private float screenHeightInUnits;

    private void Start()
    {
        adjustBOXRenderer = adjustBOX.GetComponent<SpriteRenderer>();

        

        Debug.Log("Y: " + screenHeightInUnits);
        float orthoSize = adjustBOXRenderer.bounds.size.x * Screen.height / Screen.width * 0.5f;
        camera.orthographicSize = orthoSize;


        calculateScreenHeightUnits();


        /*adjustBOX.transform.localScale = new Vector3(
            adjustBOX.transform.localScale.x,
            screenHeightInUnits * 0.75f,
            adjustBOX.transform.localScale.z);
        */

        float restBox =  (screenHeightInUnits - adjustBOXRenderer.bounds.size.y) / 2;



        camera.transform.position = new Vector3(
            camera.transform.position.x,
            camera.transform.position.y + restBox,
            camera.transform.position.z);


    }

    private void calculateScreenHeightUnits()
    {
        Rect viewportRect = camera.rect;

        // Obtener la altura del Viewport en unidades de Unity
        float viewportHeightInUnits = camera.orthographicSize * 2.0f;

        int screenHeightInPixels = Screen.height;

        // Obtener la altura del Viewport en píxeles
        int viewportHeightInPixels = Mathf.RoundToInt(screenHeightInPixels * viewportRect.height);

        // Calcular los píxeles por unidad
        float pixelsPerUnit = viewportHeightInPixels / viewportHeightInUnits;

        screenHeightInUnits = screenHeightInPixels / pixelsPerUnit;
    }

}
