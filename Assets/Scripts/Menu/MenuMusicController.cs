using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicController : MonoBehaviour
{

    public static MenuMusicController Instance { get; private set; }
    [SerializeField] AudioClip musicClip;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.clip = musicClip;
            audioSource.Play();
        }

    }

    public void stopMusic()
    {
        audioSource.Stop();
        Destroy(gameObject);
    }

    

}
