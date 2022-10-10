using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public string textureName;
    public string defaultTexture;

    public string currentPlayerTexture;

    

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeTextureDetectionSand(Player player)
    {
        if (currentPlayerTexture.Equals("Sand"))
        {
            player.DetectSand();
        }
    }

    void ChangeTextureDetectionGrass(Player player)
    {
        if (currentPlayerTexture.Equals("Grass"))
        {
            player.DetectGrass();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() != null)
        {
            currentPlayerTexture = textureName;
            ChangeTextureDetectionSand(other.GetComponent<Player>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            currentPlayerTexture = defaultTexture;
            ChangeTextureDetectionGrass(other.GetComponent<Player>());
        }
    }
}
