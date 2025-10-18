using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeeManager : MonoBehaviour
{
    public List<BeeScript> Bees = new List<BeeScript>();

    [SerializeField]public Texture NORMAL_BEE;
    [SerializeField] public Texture EXTRA_BEE;

    public static BeeManager instance;
    int lastDir = 1;

    public bool EXTRA = true;

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


    public void addBee()
    {
        for (int i = 0; i < Bees.Count; i++)
        {
            if (!Bees[i].gameObject.activeSelf)
            {
                if (lastDir == 1)
                {
                    Bees[i].Respawn(new Vector3(Screen.currentResolution.width/2 + Bees[i].spriteWidth, Bees[i].yOrigin, 0), -1, ValuesGeneratorScripts.Instance.getRandomValue(), EXTRA);
                    lastDir = -1;
                }
                else
                {
                    Bees[i].Respawn(new Vector3((-Screen.currentResolution.width/2) - Bees[i].spriteWidth, Bees[i].yOrigin, 0), 1, ValuesGeneratorScripts.Instance.getRandomValue(), EXTRA);
                    lastDir = 1;
                }

                if (EXTRA)
                {
                    EXTRA = false;
                }

                break;
            }
        }
    }

    public void disableAllBees()
    {
        for (int i = 0; i < Bees.Count; i++)
        {
            if (Bees[i].gameObject.activeSelf)
            {
                Bees[i].Despawn();
            }
        }
    }

    public void pauseBees()
    {
        for (int i = 0; i < Bees.Count; i++)
        {
            if (Bees[i].gameObject.activeSelf)
            {
                Bees[i].state = BeeScript.BeeState.PAUSED;
                Bees[i].stopAnimation();
            }
        }
    }

    public void resumeBees()
    {
        for (int i = 0; i < Bees.Count; i++)
        {
            if (Bees[i].gameObject.activeSelf)
            {
                Bees[i].state = BeeScript.BeeState.MOVING;
                Bees[i].stopAnimation();
            }
        }
    }

}
