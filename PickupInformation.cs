using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupInformation : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI weaponName;
    public CanvasGroup canvasGroup;


    public void ShowPanel()
    {
        canvasGroup.alpha = 1;
    }

    public void HidePanel()
    {
        canvasGroup.alpha = 0;
    }

    public void SetWeaponName(string name)
    {
        weaponName.text = name;
    }

}
