using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxBackroundScript : MonoBehaviour
{

    [SerializeField] RawImage parallax;
    [SerializeField] float parallaxXSpeed;
    [SerializeField] float parallaxYSpeed;
    void Update()
    {
        parallax.uvRect = new Rect(new Vector2(parallax.uvRect.x + parallaxXSpeed * Time.deltaTime, parallax.uvRect.y + parallaxYSpeed * Time.deltaTime), parallax.uvRect.size);    
    }
}
