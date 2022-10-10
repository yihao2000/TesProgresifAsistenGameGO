using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
   
    public Image fill;
    public TextMeshProUGUI text;





    public void SetMaxExp(int exp)
    {
        slider.maxValue = exp;
    }
    public void SetExp(int exp)
    {
        Debug.Log(exp);
        slider.value = exp;

    }


    public void SetCurrLevel(int currLevel)
    {
        text.text = "LVL." + currLevel;
    }
}
