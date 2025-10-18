using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] AudioClip popSfx1;
    [SerializeField] AudioClip popSfx2;
    [SerializeField] AudioClip levelup;
    [SerializeField] AudioClip levelup2;
    [SerializeField] AudioClip mareado;
    [SerializeField] AudioClip crujido;
    [SerializeField] AudioClip Notificacion02;
    [SerializeField] AudioClip Cricri;
    [SerializeField] AudioClip Splat;
    [SerializeField] AudioClip CritSolo;
    [SerializeField] AudioClip CritDoble;
    [SerializeField] AudioClip Cuacha;
    [SerializeField] AudioClip Gaviota;
    [SerializeField] AudioClip Pelota;
    [SerializeField] AudioClip Limpieza;
    [SerializeField] AudioClip Correct;


    public static SoundManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void playOnce(Enum sfx)
    {
        switch (sfx)
        {
            case SFXS.popSfx1:
                source.PlayOneShot(popSfx1);
                break;
            case SFXS.popSfx2:
                source.PlayOneShot(popSfx2);
                break;
            case SFXS.levelup:
                source.PlayOneShot(levelup);
                break;
            case SFXS.levelup2:
                source.PlayOneShot(levelup2);
                break;
            case SFXS.mareado:
                source.PlayOneShot(mareado);
                break;
            case SFXS.Crujido:
                source.PlayOneShot(crujido);
                break;
            case SFXS.Notificacion02:
                source.PlayOneShot(Notificacion02);
                break;
            case SFXS.Cricri:
                source.PlayOneShot(Cricri);
                break;
            case SFXS.splat:
                source.PlayOneShot(Splat);
                break;
            case SFXS.critsolo:
                source.PlayOneShot(CritSolo);
                break;
            case SFXS.critdoble:
                source.PlayOneShot(CritDoble);
                break;
            case SFXS.cuacha:
            source.PlayOneShot(Cuacha);
                break;
            case SFXS.gaviota:
            source.PlayOneShot(Gaviota);
                break;
            case SFXS.pelota:
            source.PlayOneShot(Pelota);
                break;
            case SFXS.limpieza:
                source.PlayOneShot(Limpieza);
                break;
            case SFXS.correct:
                source.PlayOneShot(Correct);
                break;
        }
    }

    public enum SFXS
    {
        popSfx1,
        popSfx2,
        levelup,
        levelup2,
        mareado,
        Crujido,
        Notificacion02,
        Cricri,
        splat,
        critsolo,
        critdoble,
        cuacha,
        gaviota,
        pelota,
        limpieza,
        correct
    }




}
