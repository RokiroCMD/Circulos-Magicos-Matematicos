using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiControllerMenu : MonoBehaviour
{
    [SerializeField] private float parallax_speedCloud = 1.5f;
    [SerializeField] private RawImage parallaxCloud;

    void Update()
    {
        parallaxCloud.uvRect = new Rect(parallaxCloud.uvRect.position + new Vector2(parallax_speedCloud, 0) * Time.deltaTime, parallaxCloud.uvRect.size);
    }
}
