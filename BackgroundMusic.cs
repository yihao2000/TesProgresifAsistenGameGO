using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public AudioSource gameplayBgm;
    void Start()
    {
        gameplayBgm.Play();
    }
  
}
