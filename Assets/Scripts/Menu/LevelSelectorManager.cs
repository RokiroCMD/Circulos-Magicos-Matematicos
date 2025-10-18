using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectorManager : MonoBehaviour
{
    MenuMusicController musicController;

    private void Start()
    {
        musicController = MenuMusicController.Instance;
    }

    public void stopMenuMusic()
    {
        musicController.stopMusic();
    }
}
