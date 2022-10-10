using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenuButton : MonoBehaviour
{
    // Start is called before the first frame update

    public Button mainMenuButton;
    public Button quitButton;
    void Start()
    {
    

    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public  void MainMenuButtonOnClick()
    {
            Debug.Log("Masuk ke onclick");
            Debug.Log("Masuk ke click mainmenu");
            SceneManager.LoadScene("Menu");

    }

    void QuitButtonOnClick()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
