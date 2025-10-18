using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eliminador : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mesh"))
        {
            other.gameObject.SetActive(false);
        }
        else if(other.CompareTag("Cone"))
        { 
            other.gameObject.SetActive(false);
        }
        else if(other.CompareTag("Grillo"))
        {
            TimerScript.instance.addTime(10);
            SoundManager.instance.playOnce(SoundManager.SFXS.mareado);
            other.gameObject.SetActive(false);
        }
        
    }
}
