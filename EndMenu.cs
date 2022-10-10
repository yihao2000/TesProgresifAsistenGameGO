using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMenu : MonoBehaviour
{
    // Start is called before the first frame update

    public CanvasGroup endMenu;
    public CanvasGroup playerUi;
    public Player player;

    bool isShowing;

    float endMenuDesiredAlpha, endMenuCurrentAlpha, playerUiDesiredAlpha, playerUiCurrentAlpha;
    Vector3 playerTeleport;

    private void Start()
    {
        isShowing = false;
        endMenuCurrentAlpha = 0;
        endMenuDesiredAlpha = 1;
        playerUiDesiredAlpha = 0;
        playerUiCurrentAlpha = 1;
        playerTeleport = new Vector3(0, 0, 0);

        
    }

    public void ShowPanel()
    {
        isShowing = true;
        playerUi.GetComponentInParent<Canvas>().enabled = false;

       
    }

    public void HidePanel()
    {
        endMenu.alpha = 0;
    }

    private void Update()
    {
        if (isShowing && endMenuCurrentAlpha < endMenuDesiredAlpha)
        {

            endMenuCurrentAlpha = Mathf.MoveTowards(endMenuCurrentAlpha, endMenuDesiredAlpha, 0.3f * Time.deltaTime);
            endMenu.alpha = endMenuCurrentAlpha;
            playerUiCurrentAlpha = Mathf.MoveTowards(playerUiCurrentAlpha, playerUiDesiredAlpha, 0.3f * Time.deltaTime);
            playerUi.alpha = playerUiCurrentAlpha;
        }

 
    }
}
