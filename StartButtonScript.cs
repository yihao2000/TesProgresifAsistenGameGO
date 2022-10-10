using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{

    public MainMenuAudioManager audioManager;
    public void playButton()
    {
        SceneManager.LoadScene("Game");
        audioManager.PlayButtonSoundEffect();
    }
}
