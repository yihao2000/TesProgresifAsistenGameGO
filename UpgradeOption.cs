using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeOption : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI upgradePoint;
    public TextMeshProUGUI strengthPoint;
    public TextMeshProUGUI powerPoint;
    public TextMeshProUGUI agilityPoint;


    public void ShowPanel()
    {
        canvasGroup.alpha = 1;
    }

    public void HidePanel()
    {
        canvasGroup.alpha = 0;
    }

    public void ModifyUpgradePoint(int point)
    {
        upgradePoint.text = point+"";
    }

    public void ModifyStrengthPoint(int point)
    {
        strengthPoint.text = point + "";
    }

    public void ModifyAgilityPoint(int point)
    {
        agilityPoint.text = point + "";
    }

    public void ModifyPowerPoint(int point)
    {
        powerPoint.text = point + "";
    }

    public void UpgradeStats()
    {

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
