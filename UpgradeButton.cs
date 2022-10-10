using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public string buttonType;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonOnClick);
        player = GameObject.Find("Character").GetComponent<Player>();
    }

 

    void ButtonOnClick()
    {
        Debug.Log("Keklick cok");
        player.UpgradeStats(buttonType);
    }
}