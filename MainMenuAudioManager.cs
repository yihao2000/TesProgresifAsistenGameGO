using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bgmMusic;

    public AudioSource buttonSoundEffect;
    void Start()
    {
        bgmMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonSoundEffect()
    {
        buttonSoundEffect.Play();
    }
}
